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
    public static class StringExtensions
    {
        /// <summary>
        /// Shortcut for !string.IsNullOrEmpty
        /// </summary>
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool NotEquals (this string str, string value)
        {
            return !str.Equals(value);
        }

        public static bool IsLike(this string str, string value)
        {
            return str.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool NotLike(this string str, string value)
        {
            return !str.IsLike(value);
        }

        public static bool Contains(this string str, object value)
        {
            return str.Contains(value.ToString());
        }
        public static string Part(this string str, int index, char separator='.')
        {
            return Part(str, '.', index);
        }
        public static string Part(this string str, char separator, int index)
        {
            if (str.Contains(separator))
            {
                var parts = str.Split(separator);

                if (parts.Length > index)
                    return parts[index];
            }
            return str;
        }

        public static bool ContainsUnicodeCharacter(this string s)
        {
            const int MaxAnsiCode = 255;

            return s.ToCharArray().Any(c => c > MaxAnsiCode);
        }

        public static string ToUtf8(this string s)
        {
            if (!s.ContainsUnicodeCharacter())
                return s;

            Encoding utf8 = Encoding.UTF8;
            Encoding unicode = Encoding.Unicode;

            byte[] unicodeBytes = unicode.GetBytes(s);
            byte[] utf8Bytes = Encoding.Convert(unicode, utf8, unicodeBytes);

            return utf8.GetString(utf8Bytes);
        }

        public static string Default(this string str, string defaultText = "")
        {
            if (str.HasValue())
                return str;

            return defaultText;
        }
        public static string HtmlDefault(this string str, string defaultText, string cssClass)
        {
            if (defaultText == null)
                defaultText = string.Empty;

            string format = string.Format("<span{0}>{{0}}</span>", cssClass.FormatIfNotEmpty(" class='{0}'"));

            if (string.IsNullOrEmpty(str))
                return string.Format(format, defaultText);
            else
                return str;
        }

        public static bool EndsLike(this string str, string compareToString)
        {
            if (str == null || compareToString == null)
                return false;

            return str.ToLower().EndsWith(compareToString.ToLower());
        }
        public static bool EndsWithFormat(this string str, string formatString, params object[] args)
        {
            return str.EndsWith(string.Format(formatString, args));
        }
        public static bool StartsWithFormat(this string str, string formatString, params object[] args)
        {
            return str.StartsWith(string.Format(formatString, args));
        }

        public static string TrimWhitespace(this string str)
        {
            return str.Replace("\n", "").Replace("\r", "").Replace("\t", "").Trim();
        }

        [Obsolete("Use .isEmpty() method", true)]
        public static bool IsNullOrEmpty(this string s)
        {
            return s.isEmpty();
        }
        [Obsolete("Use .isNotEmpty() method",true)]
        public static bool IsNotNullOrEmpty(this string s)
        {
            return s.isNotEmpty();
        }

        /// <summary>
        /// Removes all whitespace
        /// </summary>
        public static string Scrub(this string str)
        {
            return Regex.Replace(str, "[\n\r\t]", "").Trim();
        }

        public static bool Resembles(this string str, object obj)
        {
            return str.Resembles(obj.ToString());
        }
        public static bool Resembles(this string str, string compareToString)
        {
            if (str == null || compareToString == null)
                return false;

            return str.Trim().Equals(compareToString.Trim()
                                                , StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StartsLike(this string str, string compareToString)
        {
            if (str == null || compareToString == null)
                return false;

            return str.ToLower().StartsWith(compareToString.ToLower());
        }

        public static string DisplayIfEquals(this string str, object compareToString, string resultText)
        {
            if (str == null || compareToString == null)
                return "";

            if (str == compareToString.ToString())
                return resultText;

            return "";
        }
        
        public static string DisplayIfResembles(this string str, object compareString, string resultText)
        {
            if (str.Resembles(compareString))
                return resultText;

            return "";
        }
        public static string DisplayIfResembles(this object str, object compareString, string resultText)
        {
            return str.ToString().DisplayIfResembles(compareString, resultText);
        }

        public static bool ContainsLike(this string str, object compareToString)
        {
            return ContainsLike(str, compareToString.ToString());
        }
        public static bool ContainsLike(this string str, string compareString)
        {
            if (str.isEmpty() || compareString.isEmpty())
                return false;

            return str.ToLower().Contains(compareString.ToLower());
        }

        public static string AddIfNotEmpty(this string str, string text)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str + text;
        }

        public static string Append(this string str, char value)
        {
            return str.Append(value.ToString());
        }
        public static string Append(this string str, string suffix)
        {
            return str.Default() + suffix.Default();
        }
        public static string AppendUnique(this string str, string suffix)
        {
            if (!str.EndsWith(suffix))
            {
                str = str.Default().Append(suffix);
            }
            return str;
        }

        [Obsolete("Use Append() method", true)]
        public static string AppendSuffix(this string str, string suffix)
        {
            return str.Default() + suffix.Default();
        }
        [Obsolete("Use AppendUnique() method", true)]
        public static string AppendUniqueSuffix(this string str, string suffix)
        {
            if (!str.EndsWith(suffix))
            {
                str = str.Default().AppendSuffix(suffix);
            }
            return str;
        }

        public static string Prepend(this string str, char value)
        {
            return str.Prepend(value.ToString());
        }
        public static string Prepend(this string str, string value)
        {
            return value.Default() + str.Default();
        }
        public static string PrependUnique(this string str, string value)
        {
            if (!str.StartsWith(value))
            {
                str = str.Default().Prepend(value);
            }
            return str;
        }
        
        [Obsolete("Use Prepend() method", true)]
        public static string AppendPrefix(this string str, string value)
        {
            return value.Default() + str.Default();
        }
        [Obsolete("Use PrependUnique() method", true)]
        public static string AppendUniquePrefix(this string str, string value)
        {
            if (!str.StartsWith(value))
            {
                str = str.Default().AppendPrefix(value);
            }
            return str;
        }

        public static string Substring(this string str, string value)
        {
            int idx = str.IndexOf(value);
            return str.Substring(idx);
        }

        /// <summary>
        /// Format a string
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="formatString">The string format.  Should have 1 replaceable parameter only.</param>
        /// <example>
        /// string s = "500";
        /// string formatted = s.Format("{0} pounds!");
        /// output: "500 pounds!"
        /// </example>
        public static string FormatMe(this string s, string formatString)
        {
            return string.Format(formatString, s);
        }
        public static string Format(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static string FormatIfNotEmpty(this string str, string format)
        {
            return FormatIfNotEmpty(str, format, null);
        }
        public static string FormatIfNotEmpty(this string str, string format, params object[] args)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            List<object> list = null;

            if (args == null)
            {
                // if no args are given, assume the format only has one replaceable paramter of {0}
                // and the value is the string itself
                list = new List<object>();
                list.Add(str);
            }
            else
            {
                //otherwise, all paramters must be supplied explicitely
                list = args.ToList();
            }

            return string.Format(format, list.ToArray());
        }

        public static string Encrypt64(this string str, string secret, CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            return Encryptor.Encrypt64(str, secret, provider, cipherMode, paddingMode, IV);
        }

        public static byte[] Decrypt(this string str, string secret, CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            return Encryptor.Decrypt(str.ToByteArray(), secret, provider, cipherMode, paddingMode, IV);
        }
        public static string Decrypt64(this string str, string secret, CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            return Encryptor.Decrypt64(str, secret, provider, cipherMode, paddingMode, IV);
            //return ASCIIEncoding.ASCII.GetString(Encryptor.Decrypt(str.Decode64ToByteArray(), secret, provider, cipherMode, paddingMode)).Replace("\0","");
        }

        public static byte[] ToHexBytes(this string str)
        {
            int NumberChars = str.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            return bytes;
        }

        public static string Printf(this string str, string format)
        {
            return Printf(str, format, null);
        }
        public static string Printf(this string str, string format, params object[] args)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            if (!format.isFormatString())
                return str;

            var list = (args == null)
                        ? new List<object>()
                        : args.ToList();

            list.Insert(0, str);

            return string.Format(format, list.ToArray());
        }

        public static string AsHTML(this string s)
        {
            if (s.isEmpty())
                return "";

            return HttpUtility.HtmlEncode(s);
            //return HttpUtility.HtmlEncode(s).Replace("\n", "<br/>").Replace(" ", "&nbsp;");
        }

        public static byte[] ToByteArray(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static string Encode64(this string str)
        {
            return Convert.ToBase64String(str.ToByteArray());
        }
        public static string Decode64(this string str)
        {
            str = str.AddPaddingToString64();
            var result = Convert.FromBase64String(str);
            
            return Encoding.Default.GetString(result);
        }

        public static string Escape(this string s)
        {
            return Uri.EscapeUriString(s);
        }


        private static string AddPaddingToString64(this string str)
        {
            int padChars = (str.Length % 4) == 0 ? 0 : (4 - (str.Length % 4));

            StringBuilder result = new StringBuilder(str, str.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));

            result = result.Replace('-', '+').Replace('_', '/');
            return result.ToString();
        }

        public static string DecodeUrl(this string str)
        {
            return Uri.UnescapeDataString(str);
        }
        public static string EncodeUrl(this string str)
        {
            return Uri.EscapeUriString(str).Replace("+","%2B").Replace("=","%3D");
        }


        public static byte[] Decode64ToByteArray(this string str)
        {
            return Convert.FromBase64String(str.AddPaddingToString64());
        }

        public static string HashForKey(this string str, string secret)
        {
            var bytes = str.ToByteArray().HashForKey(secret.ToByteArray());
            return Encoding.Default.GetString(bytes);
        }

        public static Single ToSingle(this string str)
        {
            float i;
            if (float.TryParse(str, out i))
                return i;
            return 0.0F;
        }
        public static Double ToDouble(this string str)
        {
            double i;
            if (double.TryParse(str, out i))
                return i;
            return 0.0;
        }
        public static Int16 ToInt16(this string str)
        {
            short i;
            if (short.TryParse(str, out i))
                return i;
            return 0;
        }
        public static Int32 ToInt32(this string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return i;
            return 0;
        }
        public static Int64 ToInt64(this string str)
        {
            long i;
            if (long.TryParse(str, out i))
                return i;
            return 0;
        }
        public static UInt32 ToUInt32(this string str)
        {
            uint i;
            if (uint.TryParse(str, out i))
                return i;
            return 0;
        }
        public static UInt64 ToUInt64(this string str)
        {
            ulong i;
            if (ulong.TryParse(str, out i))
                return i;
            return 0;
        }

        public static T ToEnum<T>(this string value)
        {
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw new InvalidCastException();
            }
            return (T)Enum.Parse(typeof(T), value);
        }

        public static string ToValue(this PropertyItem pi, string defaultText = null, bool excludeNullChar = true)
        {
            string result;
            if (pi.Type == 1)
                result = pi.Value.ToUnicode(excludeNullChar);
            else
                result = pi.Value.ToUTF8String(excludeNullChar);

            if (defaultText != null && result.isEmpty())
                result = defaultText;

            return result;
        }

        public static DateTime ToDateTime(this PropertyItem pi)
        {
            string result = ToValue(pi);

            return DateTime.ParseExact(result, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static FileInfo ToFile(this string str)
        {
            return new FileInfo(str);
        }

        public static string FileName(this FileInfo fi)
        {
            return fi.Name.Part(0);
        }
        public static Guid ToGuid(this string value)
        {
            Guid g;
            if (Guid.TryParse(value, out g))
                return g;

            ShortGuid sg = value;

            if (sg != null)
                return sg.Guid;

            return Guid.NewGuid();
        }

        //public static string ToHex(this string str64)
        //{
        //    string hex = "";
        //    foreach (char c in str64)
        //    {
        //        int tmp = c;
        //        hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        //    }
        //    return hex;
        //}

        //public static string FromHex(this string str)
        //{
        //    string StrValue = "";
        //    while (str.Length > 0)
        //    {
        //        StrValue += System.Convert.ToChar(System.Convert.ToUInt32(str.Substring(0, 2), 16)).ToString();
        //        str = str.Substring(2, str.Length - 2);
        //    }
        //    return StrValue.Encode64();
        //}

        public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }
        public static T To<T>(this string text, T defaultReturn)
        {
            try { return (T)Convert.ChangeType(text, typeof(T)); }
            catch { return defaultReturn; }
        }
        public static bool isEmpty(this string str, bool ignoreWhitespace = true)
        {
            if (ignoreWhitespace)
                return str.Default().Trim().isEmpty(false);

            return string.IsNullOrEmpty(str);
        }
        public static bool isNotEmpty(this string str, bool ignoreWhitespace = true)
        {
            if (ignoreWhitespace)
                return str.Default().Trim().isNotEmpty(false);

            return !string.IsNullOrEmpty(str);
        }
        public static bool isFormatString(this string str)
        {
            return str.Contains("{0}");
        }

        public static bool IsIn(this string str, params object[] set)
        {
            return str.IsIn(StringComparison.InvariantCultureIgnoreCase, set);
        }
        public static bool IsIn(this string str, StringComparison comparison, params object[] set)
        {
            if (str.isEmpty())
                return false;

            foreach (var obj in set)
            {
                if (str.Equals(obj.ToString(), comparison))
                    return true;
            }
            return false;
        }

        public static bool IsNotIn(this string str, params object[] set)
        {
            return str.IsNotIn(StringComparison.InvariantCultureIgnoreCase, set);
        }
        public static bool IsNotIn(this string str, StringComparison comparison, params object[] set)
        {
            return !str.IsIn(comparison, set);
        }

        public static bool ToGuid(this string @string, out Guid ret)
        {
            try
            {
                ret = new Guid(@string);
                return true;
            }
            catch (Exception)
            {
                ret = Guid.Empty;
                return false;
            }
        }

        public static string Ellipsify(this string text, int length = 12)
        {
            if (text.Length <= length)
            {
                return text;
            }
            return text.Substring(0, length) + "...";
        }

        public static string ToAlphanumeric(this string text)
        {
            return new string(text.Where(c => c.IsLetterOrDigit()).ToArray());
        }
    }
}

namespace System.Web
{
    public static class exentsionmethods
    {
        [Obsolete("This method is depricated.  Use ToAbsoluteUrl instead.")]
        public static string ToAbsolutePath(this string str)
        {
            return ToAbsoluteUrl(str);
        }

        //convert a physical file path to absolute URL
        public static string ToUrl(this string str)
        {
            return str.ToVirtualPath().ToAbsoluteUrl().Escape();
        }
        public static string ToAbsoluteUrl(this string str)
        {
            if (str.StartsLike("http://") || str.StartsLike("https://"))
                return str;

            var req = HttpContext.Current.Request;
            string url = VirtualPathUtility.ToAbsolute(str.Replace("\\", "/"));//.EncodeUrl();
            return string.Format("{0}://{1}{2}"
                        , req.Url.Scheme, req.Url.Authority
                //, (req.Url.Port == 80) ? "" : ":" + req.Url.Port.ToString()
                        , url);

        }

        public static string ToPhysicalPath(this string virtualPath)
        {
            return System.Web.Hosting.HostingEnvironment.MapPath(virtualPath);
        }
        public static string ToVirtualPath(this string physicalPath)
        {
            return physicalPath.Replace(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "").Replace("\\", "/").Insert(0, "~/");//.EncodeUrl();
        }

        public static bool isMobileAgent(this string strUserAgent)
        {
            if (strUserAgent.isEmpty())
                return false;

            strUserAgent = strUserAgent.ToLower();

            return (
                //Request.Browser.IsMobileDevice == true ||
                strUserAgent.Contains("iphone")
                || strUserAgent.Contains("ipod")
                || strUserAgent.Contains("android")
                // || strUserAgent.Contains("mobile")
                || strUserAgent.Contains("blackberry")
                || strUserAgent.Contains("windows ce")
                || strUserAgent.Contains("opera mini")
                || strUserAgent.Contains("palm")
                //|| strUserAgent.Contains("chrome")
                //|| strUserAgent.Contains("ipad")
            );
        }
    }
}
