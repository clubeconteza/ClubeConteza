using System;
using System.Security.Cryptography;
using System.Text;

namespace Boleto.DAO
{
    public class CriptografiaDAO
    {
        private static readonly TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
        public String KeyChave = "U&4v)G$KL$Lf55";

        public static byte[] MD5Hash(string value)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(value);
            return mD5.ComputeHash(byteArray);
        }

        public string Decrypt(String encryptedString)
        {
            try
            {
                tripleDes.Key = MD5Hash("U&4v)G$KL$Lf55");
                tripleDes.Mode = CipherMode.ECB;

                byte[] buffer = Convert.FromBase64String(encryptedString);
                return Encoding.ASCII.GetString(tripleDes.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Encrypt(String stringToEncrypt)
        {
            try
            {
                {
                    return Convert.ToString(HashManagerDAO.CritpoHash(stringToEncrypt, HashType.MD5));
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string EncryptInterna(String stringToEncrypt)
        {
            try
            {
                tripleDes.Key = MD5Hash("U&4v)G$KL$Lf55");
                tripleDes.Mode = CipherMode.ECB;

                byte[] buffer = Encoding.ASCII.GetBytes(stringToEncrypt);
                KeyChave = null;
                return Convert.ToBase64String(tripleDes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}