using System;
using UnityEngine;

namespace Lab5Games
{
    [System.Serializable]
    public struct VersionCode : IEquatable<VersionCode>
    {
        public int Major;
        public int Minor;
        public int Revision;
        public string CreatedDate;

        public string Code => $"{Major}.{Minor}.{Revision}";
        public string Date => string.IsNullOrEmpty(CreatedDate) ? "" : CreatedDate;
        public string FullVersion => $"{Code}({Date})";

        public VersionCode(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            CreatedDate = string.Empty;
        }

        public VersionCode(string strVersion)
        {
            string[] codes = strVersion.Trim().Split('.');

            if (codes.Length != 3)
                throw new Exception("Invalid version code: " + strVersion);

            Major = int.Parse(codes[0]);    
            Minor = int.Parse(codes[1]);
            Revision = int.Parse(codes[2]);
            CreatedDate = string.Empty;
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
            return $"Version: {Code}";
        }

        public static bool operator ==(VersionCode a, VersionCode b) => a.Equals(b);
        public static bool operator !=(VersionCode a, VersionCode b) => !a.Equals(b);
    }
}
