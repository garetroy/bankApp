﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace bankLedger.Data
{
    public static class HashUtility
    {
        public static Tuple<string, string> ComputeHash(string plainText)
        {
            var random = new Random();
            int saltSize = random.Next(c_minSaltSize, c_maxSaltSize);
            var saltBytes = new byte[saltSize];

            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] hashWithSaltBytes = GetPbkdf2Bytes(plainText, saltBytes,
                c_iterations, c_hashSizeInBytes);

            return new Tuple<string, string>(Convert.ToBase64String(hashWithSaltBytes),
                Convert.ToBase64String(saltBytes));
        }

        public static bool VerifyHash(string plainText, string hashValue, string salt)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            if (hashWithSaltBytes.Length < c_hashSizeInBytes)
                return false;

            byte[] saltBytes = Convert.FromBase64String(salt);
            string expectedString = Convert.ToBase64String(GetPbkdf2Bytes(plainText,
                saltBytes, c_iterations, c_hashSizeInBytes));

            return hashValue == expectedString;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt,
            int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }

        private const int c_minSaltSize = 8;
        private const int c_maxSaltSize = 16;
        private const int c_hashSizeInBytes = 20;
        private const int c_iterations = 1000;
    }
}