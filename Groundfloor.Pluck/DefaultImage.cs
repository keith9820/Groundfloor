using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Kraft.Singles.Facebook.UI
{
    /// <summary>
    /// This is an instance of a default - "Choose From Ours" - image
    /// </summary>
    public class DefaultImage
    {
        public string Author;
        public string Subject;
        public double Priority;  // this comes form the EXIF Title property
        public string FileName;
        public short rating;
    }

    public static class DefaultImageStore
    {
        /// <summary>
        /// The method gets the EXIF metadata from the JPEG files found in the path
        /// and returns a list of DefaultImage instances.
        /// Image with a numeric Title value are included, others are ignored.
        /// </summary>
        /// <param name="path">AltPath is the default image path</param>
        public static List<DefaultImage> getImagesInPath(string path = "~/AltPhotos")
        {
            var ctx = HttpContext.Current;

            var results = ctx.Cache["DefaultImagesStore"] as List<DefaultImage>;

            if (results != null)
                return results;

            
            results = new List<DefaultImage>();

            path = ctx.Server.MapPath(path);

            foreach (var f in Directory.GetFiles(path))
            {
                if (!(f.EndsWith(".jpg") || f.EndsWith(".jpeg")))
                    continue;

                var img = getImage(f);
                //if (img.Priority >= 0.0)
                if (img.rating > 0)
                {
                    img.FileName = Path.GetFileName(f);
                    results.Add(img);
                }
            }
            results = results.OrderBy(x => x.Priority).ToList();
            ctx.Cache["DefaultImagesStore"] = results;
            return results;
        }

        static DefaultImage getImage(string path)
        {
            var ii = new DefaultImage();

            Image theImage = new Bitmap(path);

            // Get the PropertyItems property from image.
            PropertyItem[] propItems = theImage.PropertyItems;

            var encoding = new UTF8Encoding();
            foreach (PropertyItem propItem in propItems)
            {
                //if (propItem.Type == 3)
                //    continue;
                string val = encoding.GetString(propItem.Value);

                if (propItem.Type == 1)
                    val = Encoding.Unicode.GetString(propItem.Value);

                val = val.Replace("\0", "");
                switch (propItem.Id)
                {
                    case (int)EXIFProperty.Title:
                        try { ii.Priority = Convert.ToDouble(val); }
                        catch { ii.Priority = -1.0; }
                        break;
                    case (int)EXIFProperty.Author:
                        ii.Author = val;
                        break;
                    case (int)EXIFProperty.Subject:
                        ii.Subject = val;
                        break;
                    case (int)EXIFProperty.Rating:
                        ii.rating = BitConverter.ToInt16(propItem.Value, 0);
                        break;
                }
            }
            return ii;
        }
    }
    enum EXIFProperty
    {
        Title = 0x9c9b,
        Author = 0x9c9d,
        Subject = 0x9c9f,
        Rating = 0x4749
    }
}