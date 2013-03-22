using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace System.Xml
{
    public static class XmlExtensions
    {
        public static Int64 ToInt64(this XAttribute attribute)
        {
            return Convert.ToInt64(attribute.Value);
        }
        public static Int64 ToInt32(this XAttribute attribute)
        {
            return Convert.ToInt32(attribute.Value);
        }

        public static Int64 ToInt64(this XElement element)
        {
            return Convert.ToInt64(element.Value);
        }
        public static Int64 ToInt32(this XElement element)
        {
            return Convert.ToInt32(element.Value);
        }
    }
}
