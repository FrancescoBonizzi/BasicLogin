using System;

namespace BasicLogin.Security
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// True if two array of bytes are equal
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        bool AreEquals(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2);

        /// <summary>
        /// Generate a random array of bytes with a cryptographic random numbers generator
        /// </summary>
        /// <returns></returns>
        byte[] GenerateSalt();

        /// <summary>
        /// Returns a byte array with the hash of a string combined with its salt
        /// </summary>
        /// <param name="plaintextPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        byte[] Hash(string plaintextPassword, byte[] salt);
    }
}
