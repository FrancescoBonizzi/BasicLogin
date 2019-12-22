using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace BasicLogin.Security
{
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        public byte[] Hash(string plaintextPassword, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: plaintextPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 20_000,
                numBytesRequested: 256 / 8);
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public bool AreEquals(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }
    }
}
