using System;
using System.Text;

namespace Lab5Games
{
    public static class BitUtils
    {
        public static string GetIntBinaryString(int value)
        {
            StringBuilder sb = new StringBuilder(32);

            int pos = 31;
            int i= 0;

            while(i<32)
            {
                if ((value & (1 << i)) != 0)
                    sb.Append('1');
                else
                    sb.Append('0');
                pos--;
                i++;
            }

            return sb.ToString();   
        }

        public static string GetByteBinaryString(byte value)
        {
            StringBuilder sb = new StringBuilder(8);

            for (int indx = 0; indx < 8; indx++)
                sb.Append(GetBit(value, indx) ? '1' : '0');

            return sb.ToString();
        }

        public static bool GetBit(this int value, int position)
        {
            return (value & (1 << position)) != 0;
        }

        public static bool GetBit(this byte value, int position)
        {
            return (value & (1 << position)) != 0;
        }

        public static int SetBit(this int value, int position, bool bit)
        {
            return bit ? value |= (1 << position) : value & ~(1 << position); 
        }

        public static byte SetBit(this byte value, int position, bool bit)
        {
            byte result;
            if(bit)
            {
                result = Convert.ToByte(value | (1 << position));
            }
            else
            {
                result = Convert.ToByte(value & ~(1<<position));
            }

            return result;
        }
    }
}
