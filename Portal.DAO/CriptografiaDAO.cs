using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAO
{
    public class CriptografiaDAO
    {
        private static readonly TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
        public String KeyChave = "U&4v)G$KL$Lf55";

        public static byte[] MD5Hash(string value)
        {
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(value);
            return mD5.ComputeHash(byteArray);
        }

        public string Decrypt(String encryptedString)
        {
            try
            {
                tripleDes.Key = CriptografiaDAO.MD5Hash("U&4v)G$KL$Lf55");
                tripleDes.Mode = CipherMode.ECB;

                byte[] buffer = Convert.FromBase64String(encryptedString);
                return ASCIIEncoding.ASCII.GetString(tripleDes.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

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
                tripleDes.Key = CriptografiaDAO.MD5Hash("U&4v)G$KL$Lf55");
                tripleDes.Mode = CipherMode.ECB;

                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
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
