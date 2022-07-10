using System;

namespace Lab5Games
{
    [System.Serializable]
    public struct VersionCode : IEquatable<VersionCode>
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
            return $"({Major}.{Minor}.{Revision})";
        }

        public static bool operator ==(VersionCode a, VersionCode b) => a.Equals(b);
        public static bool operator !=(VersionCode a, VersionCode b) => !a.Equals(b);
    }
}
