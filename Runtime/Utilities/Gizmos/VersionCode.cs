using System;
using System.Runtime.Serialization;

namespace Lab5Games
{
    [System.Serializable]
    public struct VersionCode : IEquatable<VersionCode>, ISerializationSurrogate
    {
        public int Major;
        public int Minor;
        public int Revision;

        public string Version => $"{Major}.{Minor}.{Revision}";

        public VersionCode(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public VersionCode(string strVersion)
        {
            string[] codes = strVersion.Trim().Split('.');

            if (codes.Length != 3)
                throw new Exception("Invalid version code: " + strVersion);

            Major = int.Parse(codes[0]);    
            Minor = int.Parse(codes[1]);
            Revision = int.Parse(codes[2]);
        }

        public bool Equals(VersionCode other)
        {
            return Major.Equals(other.Major) && Minor.Equals(other.Minor) && Revision.Equals(other.Revision);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Revision);    
        }

        public override string ToString()
        {
            return $"Version: {Version}";
        }

        public static bool operator ==(VersionCode a, VersionCode b) => a.Equals(b);
        public static bool operator !=(VersionCode a, VersionCode b) => !a.Equals(b);

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            VersionCode data = (VersionCode)obj;
            info.AddValue("Major", data.Major);
            info.AddValue("Minor", data.Major);
            info.AddValue("Revision", data.Revision);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            VersionCode data = (VersionCode)obj;
            data.Major = info.GetInt32("Major");
            data.Minor = info.GetInt32("Minor");
            data.Revision = info.GetInt32("Revision");
            obj = data;
            return obj;
        }
    }
}
