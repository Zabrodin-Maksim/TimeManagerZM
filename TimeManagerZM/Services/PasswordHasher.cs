using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerZM.Services
{
    public class PasswordHasher
    {
        // Настройки алгоритма
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        // Метод для создания хэша пароля с солью
        public string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
                byte[] hash = pbkdf2.GetBytes(HashSize);

                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                return Convert.ToBase64String(hashBytes);
            }
        }

        // Метод для проверки пароля с сохранённым хэшем
        public bool VerifyPassword(string password, string savedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedHash);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
