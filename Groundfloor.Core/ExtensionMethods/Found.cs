
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    public static class BoolExtensionMethods
    {
        public static string ToString(this bool b, string ifTrue, string ifFalse)
        {
            return b ? ifTrue : ifFalse;
        }
    }

    public static class DictionaryExtensionMethods
    {
        public static void Add(this Dictionary<string, string> dict, string key, string value, bool predicate)
        {
            if (predicate)
                dict.Add(key, value);
        }
    }

    public static class EnumExtensionMethods
    {
        //public static string ToDescription(this Enum enumeration)
        //{
        //    return ToDescription(enumeration, null); //defalts to en-US
        //}
        //public static string ToDescription(this Enum enumeration, string cultureCode)
        //{
        //    if (cultureCode.isEmpty())
        //        cultureCode = "en-US";

        //    var type = enumeration.GetType();
        //    var memInfo = type.GetMember(enumeration.ToString());

        //    //loop through all the attributes which decorate this enum field
        //    if (null != memInfo && memInfo.Length > 0)
        //    {
        //        //if there is a DisplayAttrubite present
        //        var attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

        //        //if there is a matching culture code
        //        //then display the value of the attribute's Name property
        //        //otherwise just display the field name.
        //        if (null != attrs && attrs.Length > 0)
        //        {
        //            foreach (var attrib in attrs)
        //            {
        //                var da = (DisplayAttribute)attrib;
        //                if (da.CultureCode.Resembles(cultureCode))
        //                    return da.Name;
        //            }
        //        }
        //    }

        //    return enumeration.ToString();
        //}

        public static string ToValueString(this Enum enumeration)
        {
            var type = enumeration.GetType();
            return type.GetField(enumeration.ToString()).GetRawConstantValue().ToString();
        }

        public static int ToValue(this Enum enumeration)
        {
            var type = enumeration.GetType();
            return (int)type.GetField(enumeration.ToString()).GetRawConstantValue();
        }

        public static IDictionary<String, Int32> ToDictionary(this Enum enumeration)
        {
            int value = (int)(object)enumeration;
            var type = enumeration.GetType();
            return Enum.GetValues(type).Cast<Int32>().Where(v => (v & value) > 0).ToDictionary(field => Enum.GetName(type, field));
        }

        public static int LastValue(this Enum enumeration)
        {
            var type = enumeration.GetType();
            return Convert.ToInt32(Enum.GetValues(type).Cast<int>().Max());
        }

        public static bool In(this Enum enumeration, params object[] args)
        {
            return In(enumeration, args.Cast<int>().ToArray());
        }
        public static bool In(this Enum enumeration, params int[] args)
        {
            foreach (var arg in args)
            {
                if (enumeration.ToValue() == arg)
                    return true;
            }
            return false;
        }
        public static bool In(this Enum enumeration, params string[] args)
        {
            foreach (var arg in args)
            {
                if (enumeration.ToString() == arg)
                    return true;
            }
            return false;
        }

        public static bool NotIn(this Enum enumeration, params object[] args)
        {
            return !In(enumeration, args.Cast<int>().ToArray());
        }
        public static bool NotIn(this Enum enumeration, params int[] args)
        {
            return !In(enumeration, args);
        }
        public static bool NotIn(this Enum enumeration, params string[] args)
        {
            return !In(enumeration, args);
        }
    }

    public static class Int32ExtensionMethods
    {
        public static bool EqualsEnum(this int num, Enum @enum)
        {
            return num == @enum.ToValue();
        }

        public static string ToFormat(this int num, string format, string formatIfPositive = null, string formatIfZero = null, string formatIfNegative = null)
        {
            if (num < 0 && formatIfNegative.isNotEmpty())
                format = formatIfNegative;
            else if (num == 0 && formatIfZero.isNotEmpty())
                format = formatIfZero;
            else if (num > 0 && formatIfPositive.isNotEmpty())
                format = formatIfPositive;


            return format.Contains("{") ? string.Format(format, num) : num.ToString(format);
        }
        public static string ToFormat(this int? num, string format, string formatIfPositive = null, string formatIfZero = null, string formatIfNegative = null)
        {
            return ToFormat(num.GetValueOrDefault(), format, formatIfPositive, formatIfZero, formatIfNegative);
        }

        public static string NullIf(this int num, int compareTo)
        {
            if (num == compareTo)
                return "";

            return num.ToString();
        }
    }

    public static class DecimalExtensionMethods
    {
        public static string ToFormat(this decimal? num, string format, string formatIfPositive=null, string formatIfZero=null, string formatIfNegative=null)
        {
            num = num.GetValueOrDefault();

            if (num < 0 && formatIfNegative.isNotEmpty())
                format = formatIfNegative;
            else if (num == 0 && formatIfZero.isNotEmpty())
                format = formatIfZero;
            else if (num > 0 && formatIfPositive.isNotEmpty())
                format = formatIfPositive;


            return format.Contains("{") ? string.Format(format, num) : num.Value.ToString(format);
        }
    }

    public static class GenericExtensionMethods
    {
        public static bool IsIn<T>(this T t, params object[] set)
        {
            foreach (object obj in set)
            {
                if (t.ToString().Equals(obj.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

    }
}
