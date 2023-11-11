using System.Security.Cryptography;
using System.Text;

namespace ParkIstra.Libraries.ASP
{
    public static class AESProvider
    {
        private const string KEY = "e3rgYUJ78&6yhgb@234tgfRYHJIJHTE=LIhTTHTy6$5djuoafh8Yg8H80H7$Y8DFJh78W8Iou%gb16jF90SJHFUSD!DHS9";

        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        /// <summary>
        /// byte[] plain + byte[] key -> byte[] cipher
        /// </summary>
        /// <param name="alg"></param>
        /// <param name="blksize"></param>
        /// <param name="plain"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] Encrypt(SymmetricAlgorithm alg, int blksize, byte[] plain, byte[] key)
        {
            byte[] iv = GenIV(blksize);
            ICryptoTransform encrypt = alg.CreateEncryptor(key, iv);
            byte[] temp = encrypt.TransformFinalBlock(plain, 0, plain.Length);
            byte[] res = new byte[iv.Length + temp.Length];
            Array.Copy(iv, 0, res, 0, iv.Length);
            Array.Copy(temp, 0, res, iv.Length, temp.Length);
            return res;
        }

        /// <summary>
        /// byte[] cipher + byte[] key -> byte[] plain
        /// </summary>
        /// <param name="alg"></param>
        /// <param name="blksize"></param>
        /// <param name="cipher"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] Decrypt(SymmetricAlgorithm alg, int blksize, byte[] cipher, byte[] key)
        {
            byte[] iv = new byte[blksize];
            Array.Copy(cipher, 0, iv, 0, iv.Length);
            byte[] temp = new byte[cipher.Length - iv.Length];
            Array.Copy(cipher, iv.Length, temp, 0, temp.Length);
            ICryptoTransform decrypt = alg.CreateDecryptor(key, iv);
            return decrypt.TransformFinalBlock(temp, 0, temp.Length);
        }

        /// <summary>
        /// string plain + string key -> Base64 string cipher
        /// </summary>
        /// <param name="alg"></param>
        /// <param name="blksize"></param>
        /// <param name="plain"></param>
        /// <param name="key"></param>
        /// <param name="keysize"></param>
        /// <returns></returns>
        private static string Encrypt(SymmetricAlgorithm alg, int blksize, string plain, string key, int keysize)
        {
            return Encode(Encrypt(alg, blksize, Encoding.UTF8.GetBytes(plain), GenKey(key, keysize)));
        }

        /// <summary>
        /// Base64 string cipher + string key -> string plain
        /// </summary>
        /// <param name="alg"></param>
        /// <param name="blksize"></param>
        /// <param name="cipher"></param>
        /// <param name="key"></param>
        /// <param name="keysize"></param>
        /// <returns></returns>
        private static string Decrypt(SymmetricAlgorithm alg, int blksize, string cipher, string key, int keysize)
        {
            return Encoding.UTF8.GetString(Decrypt(alg, blksize, Decode(cipher), GenKey(key, keysize)));
        }

        public static string Encrypt(string plain)
        {
            return Encrypt(new AesCryptoServiceProvider { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 }, 16, plain, KEY, 32);
        }
        public static string Decrypt(string cipher)
        {
            return Decrypt(new AesCryptoServiceProvider { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 }, 16, cipher, KEY, 32);
        }

        #region Helpers

        /// <summary>
        /// Convert string key to byte[] key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keysize"></param>
        /// <returns></returns>
        private static byte[] GenKey(string key, int keysize)
        {
            HashAlgorithm ha = new SHA256Managed();
            byte[] hash = ha.ComputeHash(Encoding.UTF8.GetBytes(key));
            byte[] res = new Byte[keysize];
            Array.Copy(hash, 0, res, 0, keysize);
            return res;
        }
        private static string Encode(byte[] b)
        {
            return Convert.ToBase64String(b);
        }
        private static byte[] Decode(string s)
        {
            return Convert.FromBase64String(s);
        }
        private static byte[] GenIV(int size)
        {
            byte[] res = new byte[size];
            rng.GetBytes(res);
            return res;
        }

        #endregion
    }
}
