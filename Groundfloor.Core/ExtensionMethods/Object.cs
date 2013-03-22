
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    public static class ObjectExtensionMethods
    {
        public static bool IsEmpty(this object o)
        {
            return o == null || o.ToString().isEmpty();
        }
        public static bool Resembles(this object o, string compareToString)
        {
            return o.ToString().Resembles(compareToString);
        }
        public static object Default(this object o, object defaultValue)
        {
            if (o == null)
                return defaultValue;

            return o;
        }
        public static string Default(this object o, string defaultValue)
        {
            if (o == null)
                return defaultValue;

            return o.ToString();
        }
    }
}
