using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Groundfloor.Security
{
    public static partial class Encryptor
    {

        private static byte[] EncryptRijndaelManaged(string plainText, string privateKey = "",  
                                                                                 CipherMode cipherMode = CipherMode.ECB, 
                                                                                 PaddingMode paddingMode = PaddingMode.Zeros,
                                                                                 byte[] IV = null)
        {
            byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);

            using (var objHashMD5 = new MD5CryptoServiceProvider())
            {
                string strTempKey = privateKey; //.PadRight(KEY_SIZE, '\0');
                byte[] privateKeyBytes = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));

                using (var provider = new RijndaelManaged { Key = privateKeyBytes, Mode = cipherMode, Padding = paddingMode })
                {
                    if (IV != null && IV.Length > 0)
                        provider.IV = IV;

                    return provider.CreateEncryptor().TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                }
            }
        }

        private static byte[] DecryptRijndaelManaged(byte[] encryptedTextBytes, string privateKey = "",
                                                                                 CipherMode cipherMode = CipherMode.ECB,
                                                                                 PaddingMode paddingMode = PaddingMode.Zeros,
                                                                                 byte[] IV = null)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                string strTempKey = privateKey; //.PadRight(KEY_SIZE, '\0');
                byte[] privateKeyBytes = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));

                using (var provider = new RijndaelManaged { Key = privateKeyBytes, Mode = cipherMode, Padding = paddingMode })
                {
                    if (IV != null && IV.Length > 0)
                        provider.IV = IV;

                    return provider.CreateDecryptor().TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);
                }
            }
        }



        private static string CreateEncryptedHexString(string myString, string hexiv, string key)
        {
            byte[] result;
            using (RijndaelManaged alg = new RijndaelManaged { Padding = PaddingMode.Zeros, Mode = CipherMode.CBC, BlockSize = 16 * 8 })
            {
                alg.Key = ASCIIEncoding.UTF8.GetBytes(key);
                alg.IV = hexiv.ToHexBytes();

                using (ICryptoTransform encryptor = alg.CreateEncryptor(alg.Key, alg.IV))
                using (MemoryStream msStream = new MemoryStream())
                using (CryptoStream mCSWriter = new CryptoStream(msStream, encryptor, CryptoStreamMode.Write))
                using (StreamWriter mSWriter = new StreamWriter(mCSWriter))
                {
                    mSWriter.Write(myString);
                    mSWriter.Flush();
                    mCSWriter.FlushFinalBlock();
                    result = new byte[msStream.Length];
                    msStream.Position = 0;
                    msStream.Read(result, 0, (int)msStream.Length);
                }
            }
            return result.ToHex();
        }

        //private static string DecryptHexString(string myString, string hexiv, string key)
        //{
        //    byte[] encText = myString.ToHexBytes();
        //    using (RijndaelManaged alg = new RijndaelManaged { Padding = PaddingMode.Zeros, Mode = CipherMode.CBC, BlockSize = 16 * 8 })
        //    {
        //        alg.Key = ASCIIEncoding.UTF8.GetBytes(key);
        //        alg.IV = hexiv.ToHexBytes();

        //        using (ICryptoTransform encryptor = alg.CreateDecryptor(alg.Key, alg.IV))
        //        {
        //            using (MemoryStream msStream = new MemoryStream())
        //            {
        //                using (CryptoStream mCSWriter = new CryptoStream(msStream, encryptor, CryptoStreamMode.Write))
        //                {
        //                    using (StreamWriter mSWriter = new StreamWriter(mCSWriter))
        //                    {
        //                        mSWriter.Write(myString);
        //                        mSWriter.Flush();
        //                        mCSWriter.FlushFinalBlock();
        //                    }
        //                }
        //                result = new byte[msStream.Length];
        //                msStream.Position = 0;
        //                msStream.Read(result, 0, (int)msStream.Length);
        //            }
        //        }
        //    }
        //    return result.ToHex();
        //}
    }
}
