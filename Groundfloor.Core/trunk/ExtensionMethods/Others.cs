using Groundfloor;
using System.IO;
using System.Collections.Generic;
using Groundfloor.Media;
using System.Collections.Specialized;

namespace System
{
    public static class OtherExtensions
    {
        public static bool InRange(this int i, int start, int end)
        {
            return start <= i && i <= end;
        }
        public static string ToNumberName(this short i)
        {
            return ((long)i).ToNumberName();
        }
        public static string ToNumberName(this ushort i)
        {
            return ((long)i).ToNumberName();
        }
        public static string ToNumberName(this int i)
        {
            return ((long)i).ToNumberName();
        }
        public static string ToNumberName(this uint i)
        {
            return ((long)i).ToNumberName();
        }
        public static string ToNumberName(this ulong i)
        {
            return ((long)i).ToNumberName();
        }
        public static string ToNumberName(this long i)
        {
            if (i < 1000)
                return i.ToString();

            string[] orders = new string[] { "T", "B", "M", "K", "H" };
            long max = (long)Math.Pow(1000, orders.Length - 1);

            foreach(var order in orders)
            {
                if (i > max)
                    return string.Format("{0:##.##}{1}", decimal.Divide(i, max), order.ToLower());

                max /= 1000;
            }
            return "0";
        }
        public static string ToShortString(this Guid value)
        {
            ShortGuid g = value;
            return g.ToString();
        }

        public static float Default(this float value, float defaultvalue)
        {
            return value == float.NaN ? 0f : defaultvalue;
        }

        public static List<ImageMetadata> GetImageMetadata(this DirectoryInfo dir)
        {
            List<ImageMetadata> list = new List<ImageMetadata>();
          
            foreach (var file in dir.GetFiles())
            {
                if (!file.Extension.IsIn(StringComparison.InvariantCultureIgnoreCase, ".jpg", ".jpeg"))
                    continue;

                list.Add(new ImageMetadata(file));
            }
            return list;
        }

        public static Dictionary<string, string> ToDictionary(this NameValueCollection nvc)
        {
            var dict = new Dictionary<string, string>();
            foreach (string key in nvc.Keys)
            {
                dict.Add(key, nvc[key]);
            }
            return dict;
        }
        public static void RemoveWhere(this Dictionary<string, string> dict, Predicate<string> predicate)
        {
            List<string> keys = new List<string>();
            foreach (string k in dict.Keys)
                keys.Add(k);

            foreach (string key in keys)
            {
                if (predicate(key))
                    dict.Remove(key);
            }
        }
    }
}

namespace System.Web
{
    public static class WebExtensions
    {
        public static void Write(this HttpResponse response, string s, params object[] args)
        {
            response.Write(string.Format(s, args));
        }
    }
}

namespace System.Drawing
{
    public static class ImageExtensions
    {
        public static Image ScaleX2(this Image image)
        {
            return image.ScaleImage(image.Height*2, image.Width*2);
        }
        public static Image ScaleImage(this Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
        public static byte[] ToByteArray(this Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}