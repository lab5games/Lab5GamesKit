using System;
using System.Text;
using System.Linq;

namespace Lab5Games
{
    public static class BitUtils
    {
        public static string ToBinaryStringFromByte(byte x)
        {
            char[] buff = new char[8];

            for(int i=7; i>=0; i--)
            {
                int mask = 1 << i;
                buff[7 - i] = (x & mask) != 0 ? '1' : '0';
            }

            int cut_point = 4;
            StringBuilder sb = new StringBuilder(9);
            
            foreach(var b in buff)
            {
                if(cut_point == 0)
                {
                    cut_point = 4;
                    sb.Append(" ");
                }

                sb.Append(b);
                --cut_point;
            }

            return sb.ToString();
        }

        public static string ToBinaryStringFromInteger(int x)
        {
            char[] buff = new char[32];

            for(int i=31; i>=0; i--)
            {
                int mask = 1 << i;
                buff[31 - i] = (x & mask) != 0 ? '1' : '0';
            }

            int cut_point = 4;
            StringBuilder sb = new StringBuilder(39);

            foreach (var b in buff)
            {
                if (cut_point == 0)
                {
                    cut_point = 4;
                    sb.Append(" ");
                }

                sb.Append(b);
                --cut_point;
            }

            return sb.ToString();
        }

        public static int SetBit(this int x, int pos, int flag)
        {
            if (pos < 0 || pos > 32)
                throw new ArgumentOutOfRangeException();

            int mask = 1 << pos;
            return ((x & ~mask) | ((flag > 0 ? 1 : 0) << pos));
        }

        public static int GetBit(this int x, int pos)
        {
            if (pos < 0 || pos > 32)
                throw new ArgumentOutOfRangeException();

            int mask = 1 << pos;
            return (x & mask) != 0 ? 1 : 0;
        }

        public static byte ConvertToByte(string inputStr)
        {
            string str = new string(inputStr.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());

            return Convert.ToByte(str, 2);
        }

        public static byte SetBit(this byte x, int pos, int flag)
        {
            return flag > 0 ?
                Convert.ToByte(x | (1 << pos)) :
                Convert.ToByte(x & ~(1 << pos));
        }

        public static int GetBit(this byte x, int pos)
        {
            return (x & (1 << pos)) != 0 ? 1 : 0;
        }
    }
}
