using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Phnx.Shared.Extensions
{

    public static class StringHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        public static string Hash(string @string)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(@string, salt, Iterations, HashAlgorithm, KeySize);
            byte[] saltAndKey = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, saltAndKey, 0, SaltSize);
            Array.Copy(key, 0, saltAndKey, SaltSize, KeySize);
            return Convert.ToBase64String(saltAndKey);
        }

        public static bool Verify(string @string, string hashedString)
        {
            byte[] saltAndKey = Convert.FromBase64String(hashedString);
            byte[] salt = new byte[SaltSize];
            Array.Copy(saltAndKey, 0, salt, 0, SaltSize);
            byte[] key = new byte[KeySize];
            Array.Copy(saltAndKey, SaltSize, key, 0, KeySize);
            byte[] derivedKey = Rfc2898DeriveBytes.Pbkdf2(@string, salt, Iterations, HashAlgorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(key, derivedKey);
        }
    }
}