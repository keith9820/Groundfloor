using System;
using System.Security.Cryptography;
using System.Text;

namespace Groundfloor.Security
{
    public static partial class Encryptor
    {
        const int MIN_BLOCK_SIZE = 8;
        const int MIN_KEY_SIZE = 24;

        public static string Encrypt64(string plainText, string privateKey = "", CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            return Convert.ToBase64String(Encrypt(plainText, privateKey, provider, cipherMode, paddingMode, IV).TrimRight());
        }

        public static byte[] Encrypt(string plainText, string privateKey = "", CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            switch (provider)
            {
                case CryptoProviderType.DES:
                    return EncryptDES(plainText, privateKey, cipherMode, paddingMode, IV);
                case CryptoProviderType.RijndaelManaged:
                    return EncryptRijndaelManaged(plainText, privateKey, cipherMode, paddingMode, IV);
                case CryptoProviderType.TripleDES:
                default:
                    return EncryptTripleDES(plainText, privateKey, cipherMode, paddingMode, IV);
            }
        }

        public static string Decrypt64(string encryptedText, string privateKey = "", CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            return ASCIIEncoding.ASCII.GetString(Encryptor.Decrypt(encryptedText.Decode64ToByteArray(), privateKey, provider, cipherMode, paddingMode)).Replace("\0", "");
        }
        public static byte[] Decrypt(byte[] encryptedTextBytes, string privateKey = "", CryptoProviderType provider = CryptoProviderType.TripleDES, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros, byte[] IV = null)
        {
            switch (provider)
            {
                case CryptoProviderType.DES:
                    return DecryptDES(encryptedTextBytes, privateKey, cipherMode, paddingMode);
                case CryptoProviderType.RijndaelManaged:
                    return DecryptTripleDES(encryptedTextBytes, privateKey, cipherMode, paddingMode, IV);
                case CryptoProviderType.TripleDES:
                default:
                    return DecryptTripleDES(encryptedTextBytes, privateKey, cipherMode, paddingMode, IV);
            }
        }
    }

    public enum CryptoProviderType
    {
        any,
        DES,
        RijndaelManaged,
        TripleDES
    }
}
