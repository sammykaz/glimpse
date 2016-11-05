using System.Text;
using PCLCrypto;
using System;

namespace Glimpse.Core.Services.General
{
    public static class Cryptography
    {

        //minimum recommended size of salt is 8
        private const int saltSize = 8;

        //minimum recommended number of iterations is 1000
        private const int iterations = 1000;        

        /// <summary>
        /// Creates Salt with given length in bytes.
        /// </summary>
        /// <returns></returns>
        public static byte[] CreateSalt()
        {
            return WinRTCrypto.CryptographicBuffer.GenerateRandom(saltSize);
        }

        /// <summary>
        ///  Creates a derived key using different inputs
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="keyLengthInBytes"></param>
        /// <returns></returns>
        public static byte[] CreateDerivedKey(string password, byte[] salt, int keyLengthInBytes = 32)
        {
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
            return key;
        }

        /// <summary>
        /// Encrypts given password using symmetric algorithm AES
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Tuple<string,string> EncryptAes(string password)
        {
            byte[] salt = CreateSalt();
            byte[] key = CreateDerivedKey(password, salt);

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
            var bytes = WinRTCrypto.CryptographicEngine.Encrypt(symetricKey, Encoding.UTF8.GetBytes(password));

            //convert digested password and salt into base64 string for storage into DB
            var digest = Convert.ToBase64String(bytes);
            var base64salt = Convert.ToBase64String(salt);

            return new Tuple<string, string>(digest, base64salt);
        }

        /// <summary>
        /// Encrypts given password and salt using symmetric algorithm AES (this one is to confirm login information)
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptAes(string password, byte[] salt)
        {
            byte[] key = CreateDerivedKey(password, salt);

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
            var bytes = WinRTCrypto.CryptographicEngine.Encrypt(symetricKey, Encoding.UTF8.GetBytes(password));
            var digest = Convert.ToBase64String(bytes);
            return digest;
        }

    }
}
