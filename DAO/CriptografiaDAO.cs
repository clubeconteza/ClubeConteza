using System;
using System.Security.Cryptography;
using System.Text;

namespace DAO
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

        public string Decrypt(string encryptedString)
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

        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                {
                    return Convert.ToString(HashManagerDao.CritpoHash(stringToEncrypt, HashType.Md5));
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string EncryptInterna(string stringToEncrypt)
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