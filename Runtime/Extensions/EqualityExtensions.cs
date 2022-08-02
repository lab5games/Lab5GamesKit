using System;

namespace Lab5Games
{
    public static class EqualityExtensions
    {
        public static bool IsNull(this object obj)
        {
            return ReferenceEquals(obj, null);
        }

        public static bool SafeEquals(this object obj, object other)
        {
            if (IsNull(obj) && IsNull(other))
            {
                return true;
            }
            else
            {
                return ReferenceEquals(obj, other);
            }
        }
    }
}
