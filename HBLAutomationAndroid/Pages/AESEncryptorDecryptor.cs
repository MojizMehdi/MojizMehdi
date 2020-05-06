using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HBLAutomationAndroid.Pages
{
    class AESEncryptorDecryptor
    {
        public static string Encrypt(string r_PlainText, string r_Key, string r_IV)
        {
            byte[] l_plainBytes = Encoding.UTF8.GetBytes(r_PlainText);
            byte[] l_IV = Encoding.UTF8.GetBytes(r_IV);
            return Convert.ToBase64String(Encrypt(l_plainBytes, getRijndaelManaged(r_Key, l_IV)));
        }

        internal void Decrypt()
        {
            throw new NotImplementedException();
        }

        public static string Decrypt(string r_EncryptedText, string r_Key, string r_IV)
        {
            byte[] l_encryptedBytes = Convert.FromBase64String(r_EncryptedText);
            byte[] l_IV = Encoding.UTF8.GetBytes(r_IV);
            return Encoding.UTF8.GetString(Decrypt(l_encryptedBytes, getRijndaelManaged(r_Key, l_IV)));
        }
        public static string Decrypt(string r_EncryptedText, string r_Key, byte[] r_IV)
        {
            byte[] l_encryptedBytes = Convert.FromBase64String(r_EncryptedText);
            return Encoding.UTF8.GetString(Decrypt(l_encryptedBytes, getRijndaelManaged(r_Key, r_IV)));
        }

        private static RijndaelManaged getRijndaelManaged(string r_SecretKey, byte[] r_IV)
        {
            byte[] l_keyBytes = new byte[16];
            byte[] l_secretKeyBytes = Encoding.UTF8.GetBytes(r_SecretKey);
            Array.Copy(l_secretKeyBytes, l_keyBytes, Math.Min(l_keyBytes.Length, l_secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = l_keyBytes,
                IV = r_IV//Encoding.UTF8.GetBytes(Constants.BYTES_ENCODED_STRING)
            };
        }

        private static byte[] Encrypt(byte[] r_PlainBytes, RijndaelManaged r_RijndaelManaged)
        {
            return r_RijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(r_PlainBytes, 0, r_PlainBytes.Length);
        }

        private static byte[] Decrypt(byte[] r_EncryptedData, RijndaelManaged r_RijndaelManaged)
        {
            return r_RijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(r_EncryptedData, 0, r_EncryptedData.Length);
        }

        public static string Decrypt(string r_EncryptedData, string r_Key)
        {
            ICryptoTransform cryptDecrypt = GetAesManaged(r_Key).CreateDecryptor();
            byte[] l_EncryptedDataBytes = Convert.FromBase64String(r_EncryptedData);
            byte[] l_PlainBytes = cryptDecrypt.TransformFinalBlock(l_EncryptedDataBytes, 0, l_EncryptedDataBytes.Length);

            return Encoding.UTF8.GetString(l_PlainBytes);
        }
        public static string Encrypt(string r_PlainData, string r_Key)
        {
            ICryptoTransform crypt = GetAesManaged(r_Key).CreateEncryptor();
            byte[] l_PlainDataBytes = Encoding.UTF8.GetBytes(r_PlainData);
            byte[] l_EncryptedBytes = crypt.TransformFinalBlock(l_PlainDataBytes, 0, l_PlainDataBytes.Length);

            return Convert.ToBase64String(l_EncryptedBytes);
        }

        private static AesManaged GetAesManaged(string r_Key)
        {
            return new AesManaged
            {
                Key = Encoding.UTF8.GetBytes(r_Key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        }
    }
}
