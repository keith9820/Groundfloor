using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Groundfloor.Media
{
    public class ImageMetadata
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public long Size { get; private set; }
        public DateTime DateCreated {get; private set;}
        public DateTime DateModified {get; private set;}

        public float Height { get; private set; }
        public float Width { get; private set; }

        public float HorizontalResolution { get; private set; }
        public float VerticalResolution { get; private set; }

        public string Authors {get; private set;}
        public string Subject {get; private set;}
        public string Title {get; private set;}
        public short Rating {get; private set;}
        public string Comments {get; private set;}
        public DateTime DateTaken {get; private set;}
        
        public string Keywords {get; private set;}

        /// <summary>
        /// A.K.A. Keywords
        /// </summary>
        public string Tags { get { return Keywords; } }

        public ImageMetadata(FileInfo fi)
        {
            Name = fi.Name;
            Path = fi.Directory.FullName;
            Size = fi.Length;
            DateCreated = fi.CreationTime;
            DateModified = fi.LastWriteTime;

            Image theImage = new Bitmap(fi.FullName);

            VerticalResolution = theImage.VerticalResolution;
            //Height = theImage.Size.Height;
            //Height = theImage.Height;
            Height = theImage.PhysicalDimension.Height;

           HorizontalResolution = theImage.HorizontalResolution;
           //Width = theImage.Size.Width;
           //Width = theImage.Width;
           Width = theImage.PhysicalDimension.Width;

            foreach (PropertyItem propItem in theImage.PropertyItems)
            {
                //if (propItem.Type == 3) continue;
                switch (propItem.Id)
                {
                    case (int)MetadataProperty.Authors:
                    case (int)MetadataProperty.Artist:
                        Authors = propItem.ToValue();
                        break;
                    case (int)MetadataProperty.Rating:
                        Rating = BitConverter.ToInt16(propItem.Value, 0);
                        break;
                    case (int)MetadataProperty.Subject:
                        Subject = propItem.ToValue();
                        break;
                    case (int)MetadataProperty.Title:
                    case (int)MetadataProperty.ImageDescription:
                        Title = propItem.ToValue();
                        break;

                    case (int)MetadataProperty.Comment:
                        Comments = propItem.ToValue();
                        break;
                    case (int)MetadataProperty.Keywords:
                        Keywords = propItem.ToValue();
                        break;
                    case (int)MetadataProperty.Height:
                        Height = propItem.ToValue().ToInt32();
                        break;
                    case (int)MetadataProperty.Width:
                    case (int)MetadataProperty.ImageWidgth:
                        Width = propItem.ToValue().ToInt32();
                        break;
                    //case (int)MetadataProperty.DateTimeDigitized:
                    case (int)MetadataProperty.DateTaken:
                        if (propItem.ToValue().HasValue())
                            DateTaken = propItem.ToDateTime();
                        break;
                    case (int)MetadataProperty.ChrominanceTable:
                    case (int)MetadataProperty.LuminanceTable:
                        break;

                    default:
                        string val = propItem.ToValue("<empty>");
                        Debug.WriteLine(" \tid #{0} (x{0:x}) = [{1}]", propItem.Id, val);
                        break;
                }
            }
        }
    }
    public static class ImageMetadataFactory
    {
        /// <summary>
        /// The method gets the EXIF metadata from the JPEG files found in the path
        /// and returns a list of ImageMetadata instances.
        /// </summary>
        /// <param name="path">The images directory path</param>
        public static List<ImageMetadata> getImagesInPath(string path, string extensionsFilter="jpg, png", string fileName=null, int? numResults=null)
        {
            var results = new List<ImageMetadata>();

            foreach (var f in Directory.GetFiles(path))
            {
                if (!extensionsFilter.Default("*").Resembles("*"))
                {
                    var extensions = extensionsFilter.Split(',');
                    bool fileMatches = false;
                    foreach (string extension in extensions)
                    {
                        extension.PrependUnique(".");

                        if (f.EndsLike(extension.Trim()))
                        {
                            fileMatches = true;
                            break;
                        }
                    }
                    if (!fileMatches)
                        continue;
                }

                FileInfo fi = f.ToFile();
                if (fileName.HasValue() && !fileName.Resembles(fi.FileName()))
                    continue;

                results.Add(new ImageMetadata(fi));

                if (numResults.HasValue && results.Count == numResults.Value)
                    break;
            }
            return results;
        }
    }
    
    enum MetadataProperty
    {
        Title = 0x9c9b,
        Comment = 0x9c9c,
        Authors = 0x9c9d,
        Keywords = 0x9c9e,
        Subject = 0x9c9f,
        Rating = 0x4749,
        Width = 0xa002,
        Height = 0xa003,
        ImageWidgth = 0x0100,
        ImageHeight = 0x0101,
        ImageDescription = 0x10e,
        Artist = 0x13b,
        DateTaken = 0x9003,
        DateTimeDigitized = 0x9004,
        LuminanceTable = 0x5090,
        ChrominanceTable = 0x5091,
    }
}
