using System;
using System.Collections.Generic;

namespace Lab5Games
{
    public static class ListExtensions
    {
        public static T First<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (list.Count == 0)
                throw new InvalidOperationException("list is empty");

            return list[0];
        }

        public static T Last<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (list.Count == 0)
                throw new ArgumentException("list is empty");

            return list[list.Count -1];
        }

        public static List<string> GetStringList<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            List<string> result = new List<string>();

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

            return result;
        }

        public static List<T> Clone<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            List<T> cloned = new List<T>();

            for (int i = 0; i < list.Count; i++)
                cloned.Add(list[i]);

            return cloned;
        }

        public static bool ContainsNull<T>(this IList<T> list) where T : class
        {
            if (list == null)
                throw new ArgumentNullException("list");

            for (int i=0; i<list.Count; i++)
            {
                object o = list[i];
                if (ReferenceEquals(o, null)) return true;
            }

            return false;
        }

        public static void RemoveNulls<T>(IList<T> list) where T : class
        {
            if (list == null)
                throw new ArgumentNullException("list");

            for (int i=0; i<list.Count; )
            {
                if(ReferenceEquals(list[i], null))
                {
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        private static bool AddChecking<T>(this IList<T> list, T obj)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            bool success = false;

            try
            {
                if(!obj.IsNull() && !list.Contains(obj))
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

        private static bool RemoveChecking<T>(this IList<T> list, T obj)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (obj == null)
                throw new ArgumentNullException("obj");

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].SafeEquals(obj))
                {
                    list.RemoveAt(i);
                    return true;
                }

            }

            return false;
        }

        public static void Merge<T>(this IList<T> list, IList<T> other)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (other == null)
                throw new ArgumentNullException("other");

            for(int i=0; i<other.Count; i++)
            {
                list.AddChecking(other[i]);
            }
        }

        /// <summary>
        /// 取得不在 other list 中的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="other"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Unmerge<T>(this IList<T> list, IList<T> other)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (other == null)
                throw new ArgumentNullException("other");


            List<T> temp = new List<T>();

            for(int i=0; i<list.Count; i++)
            {
                T o = list[i];
                if (!other.Contains(o)) temp.Add(o);
            }

            list.Clear();

            for(int i=0; i<temp.Count; i++)
            {
                T o = temp[i];
                if (!o.IsNull()) list.Add(o);
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            int n = list.Count;
            while (n > 1)
            {
                int k = UnityEngine.Random.Range(0, n);
                T val = list[k];
                list[k] = list[n - 1];
                list[n - 1] = val;

                --n;
            }
        }

        public static void Reverse<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

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
            if (list == null)
                throw new ArgumentNullException("list");

            if (comparison == null)
                throw new ArgumentNullException("comparison");

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
            if (list == null)
                throw new ArgumentNullException("list");

            if (list.Count == 0)
                throw new InvalidOperationException("list is empty");

            return list[RandomEx.Next(0, list.Count)];
        }

        public static void ClampCount<T>(this IList<T> list, int maxAmountOfElements)
        {
            int max = UnityEngine.Mathf.Max(0, maxAmountOfElements);
            while (list.Count > 0 && list.Count > max)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
