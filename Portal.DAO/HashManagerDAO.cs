using System.Security.Cryptography;
using System.Text;


namespace Portal.DAO
{
    enum HashType
    {
        MD5, Sha1, Sha256, Sha384, Sha512
    }

    class HashManagerDAO
    {
        public static string CritpoHash(string text, HashType hashType)
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; hashBytes.Length > i; i++)
            {
                sb.Append(hashBytes[i]);
            }
            return sb.ToString();
        }
    }
}
