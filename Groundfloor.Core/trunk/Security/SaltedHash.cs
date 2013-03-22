using System;
using System.Security.Cryptography;
using System.Text;

namespace Groundfloor.Security
{
    /// <summary>
    /// Used in computing a text string hash, comparing two hashed strings for equality.
    /// Also creates a "salt" string which can be added to a password before hashing to 
    /// blunt a dictionary attack.
    /// </summary>
    /// <seealso cref="http://www.dijksterhuis.org/creating-salted-hash-values-in-c/"/>
    public class Hash
    {
        HashAlgorithm HashProvider;
        int SalthLength;

        /// <summary>
        /// The constructor takes a HashAlgorithm as a parameter.
        /// </summary>
        /// <param name="HashAlgorithm">
        /// A <see cref="HashAlgorithm"/> HashAlgorihm which is derived from HashAlgorithm. C# provides
        /// the following classes: SHA1Managed,SHA256Managed, SHA384Managed, SHA512Managed and MD5CryptoServiceProvider
       /// and a Salt of 4 bytes ( or 4*8 = 32 bits)
        /// </param>
        public Hash(HashAlgorithm HashAlgorithm = null, int theSaltLength = 8)
        {
            HashProvider = HashAlgorithm ?? new SHA256Managed();
            SalthLength = theSaltLength;
        }


        /// <summary>
        /// The routine provides a wrapper around the GetHashAndSalt function providing conversion
        /// from the required byte arrays to strings. Both the Hash and Salt are returned as Base-64 encoded strings.
        /// </summary>
        /// <param name="Data">
        /// A <see cref="System.String"/> string containing the data to hash
        /// </param>
        /// <param name="Hash">
        /// A <see cref="System.String"/> base64 encoded string containing the generated hash
        /// </param>
        /// <param name="Salt">
        /// A <see cref="System.String"/> base64 encoded string containing the generated salt
        /// </param>
        public void GetHashAndSaltString(string clearPassword, out string Hash, out string Salt)
        {
            byte[] HashOut;
            byte[] SaltOut;

            // Obtain the Hash and Salt for the given string
            GetHashAndSalt(Encoding.UTF8.GetBytes(clearPassword), out HashOut, out SaltOut);

            // Transform the byte[] to Base-64 encoded strings
            Hash = Convert.ToBase64String(HashOut);
            Salt = Convert.ToBase64String(SaltOut);
        }
        /// <summary>
        /// Given a salt value, generate the hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetHashString(string clearPassword, string salt)
        {
            byte[] _data = Encoding.UTF8.GetBytes(clearPassword);
            //byte[] _salt = Encoding.UTF8.GetBytes(salt);
            byte[] _salt = null;
            if (salt != null)
            {
                try
                {
                    _salt = Convert.FromBase64String(salt);
                }
                catch
                {
                    salt = Convert.ToBase64String(Encoding.UTF8.GetBytes(salt));
                    _salt = Convert.FromBase64String(salt);
                }
            }

            return Convert.ToBase64String(ComputeHash(_data, _salt));
        }

        /// <summary>
        /// This routine provides a wrapper around VerifyHash converting the strings containing the
        /// data, hash and salt into byte arrays before calling VerifyHash.
        /// </summary>
        /// <param name="Data">A UTF-8 encoded string containing the data to verify</param>
        /// <param name="Hash">A base-64 encoded string containing the previously stored hash</param>
        /// <param name="Salt">A base-64 encoded string containing the previously stored salt</param>
        /// <returns></returns>
        public bool VerifyHashString(string Data, string Hash, string Salt)
        {
            byte[] HashToVerify = Convert.FromBase64String(Hash);
            byte[] SaltToVerify = Convert.FromBase64String(Salt);
            byte[] DataToVerify = Encoding.UTF8.GetBytes(Data);
            return VerifyHash(DataToVerify, HashToVerify, SaltToVerify);
        }


        /// <summary>
        /// Given a data block this routine returns both a Hash and a Salt
        /// </summary>
        /// <param name="Data">
        /// A <see cref="System.Byte"/>byte array containing the data from which to derive the salt
        /// </param>
        /// <param name="Hash">
        /// A <see cref="System.Byte"/>byte array which will contain the hash calculated
        /// </param>
        /// <param name="Salt">
        /// A <see cref="System.Byte"/>byte array which will contain the salt generated
        /// </param>
        public void GetHashAndSalt(byte[] Data, out byte[] Hash, out byte[] Salt)
        {
            Salt = GenerateSalt();
            // Compute hash value of our data with the salt.
            Hash = ComputeHash(Data, Salt);
        }

        /// <summary>
        /// This routine verifies whether the data generates the same hash as we had stored previously
        /// </summary>
        /// <param name="Data">The data to verify </param>
        /// <param name="Hash">The hash we had stored previously</param>
        /// <param name="Salt">The salt we had stored previously</param>
        /// <returns>True on a succesfull match</returns>
        public bool VerifyHash(byte[] Data, byte[] Hash, byte[] Salt)
        {
            byte[] NewHash = ComputeHash(Data, Salt);

            //  No easy array comparison in C# -- we do the legwork
            if (NewHash.Length != Hash.Length) return false;

            for (int i = 0; i < Hash.Length; i++)
                if (!Hash[i].Equals(NewHash[i]))
                    return false;

            return true;
        }

        public string GenerateSaltString()
        {
            return Convert.ToBase64String(GenerateSalt());
        }
        public byte[] GenerateSalt()
        {
            // Allocate memory for the salt
            byte[] Salt = new byte[SalthLength];

            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(Salt);

            return Salt;
        }

        private byte[] ComputeHash(string Data, string Salt)
        {
            byte[] _data = Convert.FromBase64String(Data);
            byte[] _salt = Convert.FromBase64String(Salt);

            return ComputeHash(_data, _salt);
        }
        /// <summary>
        /// The actual hash calculation is shared by both GetHashAndSalt and the VerifyHash functions
        /// </summary>
        /// <param name="Data">A byte array of the Data to Hash</param>
        /// <param name="Salt">A byte array of the Salt to add to the Hash</param>
        /// <returns>A byte array with the calculated hash</returns>
        private byte[] ComputeHash(byte[] Data, byte[] Salt)
        {
            // Allocate memory to store both the Data and Salt together
            int saltLength = Salt == null ? 0 : Salt.Length;

            byte[] DataAndSalt = new byte[Data.Length + saltLength];

            // Copy both the data and salt into the new array
            Array.Copy(Data, DataAndSalt, Data.Length);
            if (Salt != null)
                Array.Copy(Salt, 0, DataAndSalt, Data.Length, Salt.Length);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            return HashProvider.ComputeHash(DataAndSalt);
        }

        public static string ToHash(string plainText, string salt=null, int saltLength=8, HashAlgorithm algorithm = null)
        {
            Hash hash = new Hash(algorithm, saltLength);
            return hash.GetHashString(plainText, salt);
        }
        
        public static string ToHexit(string plainText, string salt = null, int saltLength = 8, HashAlgorithm algorithm = null)
        {
            string hash = ToHash(plainText, salt, saltLength, algorithm);
            byte[] arr = Convert.FromBase64String(hash);

            StringBuilder sb = new StringBuilder(arr.Length * 2);
            foreach (Byte b in arr)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}
