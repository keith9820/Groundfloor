using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Groundfloor;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using Groundfloor.Security;

namespace System
{
    public static class ByteExtensions
    {
        public static byte[] HashForKey(this byte[] dataToSign, byte[] keyBody)
        {
            using (HMACSHA256 hmacAlgorithm = new HMACSHA256(keyBody))
            {
                hmacAlgorithm.ComputeHash(dataToSign);
                return hmacAlgorithm.Hash;
            }
        }

        public static string ToASCIIString(this byte[] str, bool excludeNullChar = true)
        {
            var encoding = new ASCIIEncoding();
            var result = encoding.GetString(str);

            if (excludeNullChar)
                result = result.Replace("\0", "");
            return result;
        }
        public static string ToUTF8String(this byte[] str, bool excludeNullChar = true)
        {
            var encoding = new UTF8Encoding();
            var result = encoding.GetString(str);

            if (excludeNullChar)
                result = result.Replace("\0", "");
            return result;
        }
        public static string ToUnicode(this byte[] str, bool excludeNullChar = true)
        {
            var result = Encoding.Unicode.GetString(str);
            if (excludeNullChar)
                result = result.Replace("\0", "");
            return result;
        }

        public static string ToString64(this byte[] ba)
        {
            return Convert.ToBase64String(ba);
        }
        public static string ToHex(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static byte[] PadRight(this byte[] ba, int minSize, char paddingChar = '\0')
        {
            if (minSize % 8 > 0)
                minSize += 8 - ba.Length % 8;

            byte[] buffer = new byte[minSize];
            if (ba.Length < buffer.Length)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = i < ba.Length ? ba[i] : (byte)paddingChar;
                }
                return buffer;
            }
            return ba;
        }
        public static byte[] PadRight(this byte[] ba, char paddingChar = '\0')
        {
            int totalSize = ba.Length;
            
            if (totalSize % 8 > 0)
                totalSize += 8 - ba.Length % 8;

            byte[] buffer = new byte[totalSize];
            if (ba.Length < buffer.Length)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = i < ba.Length ? ba[i] : (byte)paddingChar;
                }
                return buffer;
            }
            return ba;
        }

        public static byte[] TrimRight(this byte[] ba, char trimCharacter = '\0')
        {
            var result = ba.TakeWhile((v, idx) => ba.Skip(idx).Any(w => w != 0x00)).ToArray();
            return result;
        }
    }
}
