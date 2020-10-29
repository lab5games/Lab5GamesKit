using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5Games
{
    public static class ListExtension
    {
        public static Type GetElementType(this IList list)
        {
            Type mType = null;
            Type arrayType = list != null ? list.GetType() : null;

            if (arrayType != null)
            {
                if (arrayType.IsArray)
                {
                    mType = arrayType.GetElementType();
                }
                else
                {
                    Type[] interfaceTypes = arrayType.GetInterfaces();
                    foreach (var iType in interfaceTypes)
                    {
                        string name = iType.Name;
                        if (name.Contains("IList`1") ||
                            name.Contains("ICollection`1") ||
                            name.Contains("IEnumerable`1"))

                        {
                            try
                            {
                                mType = iType.GetGenericArguments()[0];
                                break;
                            }
                            catch { }
                        }
                    }
                }
            }

            return mType;
        }

        public static T GetFirst<T>(this IList<T> list)
        {
            T first = default(T);
            if (list != null && list.Count > 0)
                first = list[0];

            return first;
        }

        public static T GetLast<T>(this IList<T> list)
        {
            T last = default(T);
            if (list != null && list.Count > 0)
                last = list[list.Count - 1];

            return last;
        }

        public static List<string> GetStringList<T>(this IList<T> list)
        {
            List<string> result = new List<string>();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    object o = list[i];
                    string str;

                    try
                    {
                        str = o.ToString();
                    }
                    catch
                    {
                        str = "NULL";
                    }

                    result.Add(str);
                }
            }

            return result;
        }

        public static List<T> CloneList<T>(this IList<T> list)
        {
            List<T> cloned = new List<T>();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                    cloned.Add(list[i]);
            }

            return cloned;
        }

        public static bool ContainsNull<T>(this IList<T> list) where T : class
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    object o = list[i];
                    if (o == null) return true;
                }
            }

            return false;
        }

        public static void RemoveNulls<T>(this IList<T> list) where T : class
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count;)
                {
                    if (list[i].IsNull())
                    {
                        list.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public static bool AddChecking<T>(this IList<T> list, T obj)
        {
            bool success = false;
            try
            {
                if (list != null &&
                    !obj.IsNull() &&
                    !list.Contains(obj))
                {
                    list.Add(obj);
                    success = true;
                }
            }
            catch
            {

            }

            return success;
        }
        
        public static bool RemoveChecking<T>(this IList<T> list, T obj)
        {
            if (list != null &&
                !obj.IsNull())
            {
                for (int i = 0; i < list.Count;)
                {
                    try
                    {
                        if (list[i].SafeEquals(obj))
                        {
                            list.RemoveAt(i);
                            return true;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    catch
                    {

                    }
                }
            }

            return false;
        }
        // 聯集
        public static void MergeList<T>(this IList<T> list, IList<T> other)
        {
            if (list != null && other != null)
            {
                for (int i = 0; i < other.Count; i++)
                    list.AddChecking(other[i]);
            }
        }
        // 差集
        public static void UnmergeList<T>(this IList<T> list, IList<T> other)
        {
            if (list != null && other != null)
            {
                List<T> tmpList = new List<T>();

                for (int i = 0; i < list.Count; i++)
                {
                    T o = list[i];
                    if (!other.Contains(o)) tmpList.Add(o);
                }

                list.Clear();

                for (int i = 0; i < tmpList.Count; i++)
                {
                    try
                    {
                        T o = tmpList[i];
                        if (!o.IsNull()) list.Add(o);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T val = list[k];
                list[k] = list[n];
                list[n] = val;
            }
        }

        public static void Reverse<T>(this IList<T> list)
        {
            int start = 0;
            int end = list.Count - 1;

            while (end < start)
            {
                T tmp = list[start];
                list[start] = list[end];
                list[end] = tmp;

                start++;
                end--;
            }
        }

        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (comparison.Invoke(list[i - 1], list[i]) > 0)
                {
                    T tmp = list[i];
                    list[i] = list[i - 1];
                    list[i - 1] = tmp;
                }
            }
        }

        public static T RandomElement<T>(this IList<T> list)
        {
            if (list != null)
            {
                int indx = UnityEngine.Random.Range(0, list.Count);
                if (list.Count > indx)
                    return list[indx];
            }

            return default(T);
        }

        public static void ClampToCount<T>(this IList<T> list, int maxAmountOfElements)
        {
            int max = UnityEngine.Mathf.Max(0, maxAmountOfElements);
            while (list.Count > 0 && list.Count > max)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
