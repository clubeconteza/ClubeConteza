
using System.Security.Cryptography;
using System.Text;


namespace DAO
{
    enum HashType
    {
        Md5, Sha1, Sha256, Sha384, Sha512
    }

    class HashManagerDao
    {
        public static string CritpoHash(string text, HashType hashType)
        {
            byte[] sourceBytes = Encoding.Default.GetBytes(text);
            byte[] hashBytes = null;
            switch (hashType)
            {
                case HashType.Md5:
                    hashBytes = MD5.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha1:
                    hashBytes = SHA1.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha256:
                    hashBytes = SHA256.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha384:
                    hashBytes = SHA384.Create().ComputeHash(sourceBytes);
                    break;
                case HashType.Sha512:
                    hashBytes = SHA512.Create().ComputeHash(sourceBytes);
                    break;
          
            }
            var sb = new StringBuilder();
            if (hashBytes == null) return sb.ToString();
            for (int i = 0; hashBytes.Length > i; i++)
            {
                sb.Append(hashBytes[i]);
            }
            return sb.ToString();
        }
    }
}