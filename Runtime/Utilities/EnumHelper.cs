using System;

namespace Lab5Games
{ 
    public static class EnumHelper
    {
        public static T GetRandomValue<T>() where T : Enum
        {
            T[] arr = (T[])Enum.GetValues(typeof(T));
            return arr[RandomEx.Next(0, arr.Length)];
        }

        public static T Next<T>(T value) where T : Enum
        {
            T[] arr = (T[])Enum.GetValues(typeof(T));
            int j = Array.IndexOf(arr, value) + 1;
            return (j == arr.Length) ? arr[0] : arr[j];
        }

        public static T Previous<T>(T value) where T : Enum
        {
            T[] arr = (T[])Enum.GetValues(typeof(T));
            int j = Array.IndexOf(arr, value) - 1;
            return (0 > j) ? arr[arr.Length-1] : arr[j];
        }
    }
}
