using System;
using System.Security.Cryptography;
using System.Text;

namespace PortalClubeConteza.DAO
{
    public class CriptografiaDAO
    {
        private static readonly TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
        public String KeyChave = "U&4v)G$KL$Lf55";

        public enum HashType
        {
            MD5, Sha1, Sha256, Sha384, Sha512
        }

        public static byte[] MD5Hash(string value)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(value);
            return mD5.ComputeHash(byteArray);
        }

        public string Decrypt(string encryptedString)
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
                    return Convert.ToString(CritpoHash(stringToEncrypt, HashType.MD5));
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

        public string CritpoHash(string text, HashType hashType)
        {
            byte[] sourceBytes = Encoding.Default.GetBytes(text);
            byte[] hashBytes = null;
            switch (hashType)
            {
                case HashType.MD5:
                    hashBytes = MD5CryptoServiceProvider.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha1:
                    hashBytes = SHA1Managed.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha256:
                    hashBytes = SHA256Managed.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha384:
                    hashBytes = SHA384Managed.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha512:
                    hashBytes = SHA512Managed.Create().ComputeHash(sourceBytes);
                    break;
                default:
                    break;
            }
            var sb = new StringBuilder();
            for (int i = 0; hashBytes.Length > i; i++)
            {
                sb.Append(hashBytes[i]);
            }
            return sb.ToString();
        }

        public string ValidarChave(string chave)
        {
            string retorno = "Erro";

            if (string.IsNullOrEmpty(chave))
            {
                return retorno;
            }

            try
            {
                string[] parametros = Decrypt(chave).Split(';');
                retorno = parametros[0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
    }
}