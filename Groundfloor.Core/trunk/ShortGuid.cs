//http://www.singular.co.nz/blog/archive/2007/12/20/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp.aspx
using System;

namespace Groundfloor
{
    /// <summary>
    /// Represents a globally unique identifier (GUID) with a 
    /// shorter string value. Sguid
    /// </summary>
    public struct ShortGuid
    {
        #region Static

        /// <summary>
        /// A read-only instance of the ShortGuid class whose value 
        /// is guaranteed to be all zeroes. 
        /// </summary>
        public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

        public static bool TryParse(string value, out ShortGuid sguid)
        {
            if (string.IsNullOrEmpty(value))
            {
                sguid = null;
                return false;
            }

            try
            {
                sguid = new ShortGuid(value);
                return true;
            }
            catch
            {
                sguid = ShortGuid.Empty;
                return false;
            }
        }
        #endregion

        #region Fields

        Guid _guid;
        string _value;

        #endregion

        #region Contructors

        /// <summary>
        /// Creates a ShortGuid from a base64 encoded string
        /// </summary>
        /// <param name="value">The encoded guid as a 
        /// base64 string</param>
        public ShortGuid(string value)
        {
            _value = value;
            _guid = Decode(value);
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid
        /// </summary>
        /// <param name="guid">The Guid to encode</param>
        public ShortGuid(Guid guid)
        {
            _value = Encode(guid);
            _guid = guid;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/sets the underlying Guid
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
            set
            {
                if (value != _guid)
                {
                    _guid = value;
                    _value = Encode(value);
                }
            }
        }

        /// <summary>
        /// Gets/sets the underlying base64 encoded string
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    _guid = Decode(value);
                }
            }
        }

        public bool HasValue
        {
            get
            {
                try
                {
                    return (Guid != Guid.Empty);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        #region ToString

        /// <summary>
        /// Returns the base64 encoded guid as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }

        #endregion

        #region Equals

        /// <summary>
        /// Returns a value indicating whether this instance and a 
        /// specified Object represent the same type and value.
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is ShortGuid)
                return _guid.Equals(((ShortGuid)obj)._guid);
            if (obj is Guid)
                return _guid.Equals((Guid)obj);
            if (obj is string && !string.IsNullOrEmpty(obj.ToString()))
                return _guid.Equals(((ShortGuid)obj)._guid);
            return false;
        }

        #endregion

        #region GetHashCode

        /// <summary>
        /// Returns the HashCode for underlying Guid.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        #endregion

        #region NewGuid

        /// <summary>
        /// Initialises a new instance of the ShortGuid class
        /// </summary>
        /// <returns></returns>
        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }

        #endregion

        #region Encode

        /// <summary>
        /// Creates a new instance of a Guid using the string value, 
        /// then returns the base64 encoded version of the Guid.
        /// </summary>
        /// <param name="value">An actual Guid string (i.e. not a ShortGuid)</param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            Guid guid = new Guid(value);
            return Encode(guid);
        }

        /// <summary>
        /// Encodes the given Guid as a base64 string that is 22 
        /// characters long.
        /// </summary>
        /// <param name="guid">The Guid to encode</param>
        /// <returns></returns>
        public static string Encode(Guid guid)
        {
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "$");
                //.Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        #endregion

        #region Decode

        /// <summary>
        /// Decodes the given base64 string
        /// </summary>
        /// <param name="value">The base64 encoded string of a Guid</param>
        /// <returns>A new Guid</returns>
        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("$", "+");
                //.Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Determines if both ShortGuids have the same underlying 
        /// Guid value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(ShortGuid x, ShortGuid y)
        {
            if ((object)x == null) return (object)y == null;
            return x._guid == y._guid;
        }

        /// <summary>
        /// Determines if both ShortGuids do not have the 
        /// same underlying Guid value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(ShortGuid x, ShortGuid y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Implicitly converts the ShortGuid to it's string equivilent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator string(ShortGuid shortGuid)
        {
            return shortGuid._value;
        }

        /// <summary>
        /// Implicitly converts the ShortGuid to it's Guid equivilent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator Guid(ShortGuid shortGuid)
        {
            return shortGuid._guid;
        }

        /// <summary>
        /// Implicitly converts the string to a ShortGuid
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator ShortGuid(string shortGuid)
        {
            return new ShortGuid(shortGuid);
        }

        /// <summary>
        /// Implicitly converts the Guid to a ShortGuid 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static implicit operator ShortGuid(Guid guid)
        {
            return new ShortGuid(guid);
        }

        #endregion
    }
}
/*
Using the ShortGuid

The ShortGuid is compatible with normal Guid's and other ShortGuid strings. Let's see an example:

Guid guid = Guid.NewGuid();

ShortGuid sguid1 = guid; // implicitly cast the guid as a shortguid

Console.WriteLine( sguid1 );

Console.WriteLine( sguid1.Guid );

This produces a new guid, uses that guid to create a ShortGuid, and displays the two equivalent values in the console. Results would be something along the lines of:

FEx1sZbSD0ugmgMAF_RGHw
b1754c14-d296-4b0f-a09a-030017f4461f

Or you can implicitly cast a string to a ShortGuid as well.

string code = "Xy0MVKupFES9NpmZ9TiHcw";

ShortGuid sguid2 = code; // implicitly cast the string as a shortguid

Console.WriteLine( sguid2 );

Console.WriteLine( sguid2.Guid );

Which produces the following:

Xy0MVKupFES9NpmZ9TiHcw
540c2d5f-a9ab-4414-bd36-9999f5388773
Flexible with your other data types

The ShortGuid is made to be easily used with the different types, so you can simplify your code. Take note of the following examples:

// for a new ShortGuid, just like Guid.NewGuid()

ShortGuid sguid = ShortGuid.NewGuid();

 

// to cast the string "myString" as a ShortGuid,

string myString = "Xy0MVKupFES9NpmZ9TiHcw";

 

// the following 3 lines are equivilent

ShortGuid sguid = new ShortGuid( myString ); // traditional

ShortGuid sguid = (ShortGuid)myString; // explicit cast

ShortGuid sguid = myString; // implicit cast

 

// Likewise, to cast the Guid "myGuid" as a ShortGuid

Guid myGuid = new Guid( "540c2d5f-a9ab-4414-bd36-9999f5388773" );

 

// the following 3 lines are equivilent

ShortGuid sguid = new ShortGuid( myGuid ); // traditional

ShortGuid sguid = (ShortGuid)myGuid; // explicit cast

ShortGuid sguid = myGuid; // implicit cast

After you've created your ShortGuid's the 3 members of most interest are the original Guid value, the new short string (the short encoded guid string), and the ToString() method, which also returns the short encoded guid string.

sguid.Guid; // gets the Guid part

sguid.Value; // gets the encoded Guid as a string

sguid.ToString(); // same as sguid.Value
Easy comparison with guid's and strings

You can also do equals comparison against the three types, Guid, string and ShortGuid like in the following example:

Guid myGuid = new Guid( "540c2d5f-a9ab-4414-bd36-9999f5388773" );

ShortGuid sguid = (ShortGuid)"Xy0MVKupFES9NpmZ9TiHcw";

 

if( sguid == myGuid )

  // logic if guid and sguid are equal

 

if( sguid == "Xy0MVKupFES9NpmZ9TiHcw" )

  // logic if string and sguid are equal

 */