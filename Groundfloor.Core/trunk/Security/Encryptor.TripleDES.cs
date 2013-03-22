using System;
using System.Security.Cryptography;
using System.Text;

namespace Groundfloor.Security
{
    public static partial class Encryptor
    {
        private static byte[] EncryptTripleDES(string plainText, string privateKey = "",  
                                                                                 CipherMode cipherMode = CipherMode.ECB, 
                                                                                 PaddingMode paddingMode = PaddingMode.Zeros,
                                                                                 byte[] IV = null)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText).PadRight(MIN_BLOCK_SIZE);
            using (var objHashMD5 = new MD5CryptoServiceProvider())
            {
                string strTempKey = privateKey.PadRight(MIN_KEY_SIZE, '\0');
                byte[] key = objHashMD5.ComputeHash(Encoding.UTF8.GetBytes(strTempKey));

                using (var provider = new TripleDESCryptoServiceProvider { Key = key.PadRight(MIN_KEY_SIZE), Mode = cipherMode, Padding = paddingMode })
                {
                    if (IV != null && IV.Length > 0)
                        provider.IV = IV;

                    return provider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private static byte[] DecryptTripleDES(byte[] encryptedTextBytes, string privateKey = "",
                                                                                 CipherMode cipherMode = CipherMode.ECB,
                                                                                 PaddingMode paddingMode = PaddingMode.Zeros,
                                                                                 byte[] IV = null)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                string strTempKey = privateKey.PadRight(MIN_KEY_SIZE, '\0');
                byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(strTempKey));

                using (var provider = new TripleDESCryptoServiceProvider { Key = key.PadRight(MIN_KEY_SIZE), Mode = cipherMode, Padding = paddingMode })
                {
                    if (IV != null && IV.Length > 0)
                        provider.IV = IV;

                    return provider.CreateDecryptor().TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);
                }
            }
        }
    }
}
