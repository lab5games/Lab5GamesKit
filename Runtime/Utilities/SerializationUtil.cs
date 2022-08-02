using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Lab5Games
{
    public static class SerializationUtil
    {
        static SurrogateSelector _surrogate;

        static SurrogateSelector SurrogateSelector
        {
            get
            {
                if(_surrogate == null)
                {
                    var vectorSurrogate = new VectorSerializationSurrogate();
                    var quaternionSurrogate = new QuaternionSerializationSurrogate();

                    _surrogate = new SurrogateSelector();
                    _surrogate.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vectorSurrogate);
                    _surrogate.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vectorSurrogate);
                    _surrogate.AddSurrogate(typeof(Vector4), new StreamingContext(StreamingContextStates.All), vectorSurrogate);

                    _surrogate.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), vectorSurrogate);
                }

                return _surrogate;
            }
        }

        static SerializationUtil()
        {
            // why? http://answers.unity3d.com/questions/30930/why-did-my-binaryserialzer-stop-working.html?sort=oldest
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }

        public static T DeserializeObjectFromFile<T>(string filename, string folder = null) where T : class
        {
            folder = folder ?? Application.persistentDataPath;
            var filepath = Path.Combine(folder, filename);

            if (!File.Exists(filepath))
                return null;

            using(var fileStream = File.Open(filepath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                formatter.Binder = new VersionSerializationBinder();
                formatter.SurrogateSelector = SurrogateSelector;

                return formatter.Deserialize(fileStream) as T;
            }
        }

        public static void SerializeObjectToFile(object obj, string filename, string folder = null)
        {
            folder = folder ?? Application.persistentDataPath;
            var filepath = Path.Combine(folder, filename);

            using(var fileStream = File.Open(filepath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Binder = new VersionSerializationBinder();
                formatter.SurrogateSelector = SurrogateSelector;

                formatter.Serialize(fileStream, obj);
            }
        }

        sealed class VectorSerializationSurrogate : ISerializationSurrogate
        {
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                if(obj is Vector2)
                {
                    var v2 = (Vector2)obj;
                    info.AddValue("x", v2.x);
                    info.AddValue("y", v2.y);
                }
                else if(obj is Vector3)
                {
                    var v3 = (Vector3)obj;
                    info.AddValue("x", v3.x);
                    info.AddValue("y", v3.y);
                    info.AddValue("z", v3.z);
                }
                else if(obj is Vector4)
                {
                    var v4 = (Vector4)obj;
                    info.AddValue("x", v4.x);
                    info.AddValue("y", v4.y);
                    info.AddValue("z", v4.z);
                    info.AddValue("w", v4.w);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                if (obj is Vector2)
                {
                    var v2 = (Vector2)obj;
                    v2.x = (float)info.GetValue("x", typeof(float));
                    v2.y = (float)info.GetValue("y", typeof(float));
                    return v2;
                }
                else if (obj is Vector3)
                {
                    var v3 = (Vector3)obj;
                    v3.x = (float)info.GetValue("x", typeof(float));
                    v3.y = (float)info.GetValue("y", typeof(float));
                    v3.z = (float)info.GetValue("z", typeof(float));
                    return v3;
                }
                else if (obj is Vector4)
                {
                    var v4 = (Vector4)obj;
                    v4.x = (float)info.GetValue("x", typeof(float));
                    v4.y = (float)info.GetValue("y", typeof(float));
                    v4.z = (float)info.GetValue("z", typeof(float));
                    v4.w = (float)info.GetValue("w", typeof(float));
                    return v4;
                }
                else
                {
                    
                    throw new ArgumentException();
                }
            }
        }

        sealed class QuaternionSerializationSurrogate : ISerializationSurrogate
        {
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                if(obj is Quaternion)
                {
                    Quaternion q = (Quaternion)obj;
                    info.AddValue("x", q.x);
                    info.AddValue("y", q.y);
                    info.AddValue("z", q.z);
                    info.AddValue("w", q.w);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                if(obj is Quaternion)
                {
                    Quaternion q = (Quaternion)obj;
                    q.x = (float)info.GetValue("x", typeof(float));
                    q.y = (float)info.GetValue("y", typeof(float));
                    q.z = (float)info.GetValue("z", typeof(float));
                    q.w = (float)info.GetValue("w", typeof(float));
                    return q;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        sealed class VersionSerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
                {
                    assemblyName = Assembly.GetExecutingAssembly().FullName;
                    return Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                }

                return null;
            }
        }
    }
}
