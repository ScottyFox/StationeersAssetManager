using System;
using System.Security.Cryptography;
using System.Text;

namespace SFXArtAssetManager
{
    public class Utils
    {
        public static T[] CombineArrays<T>(T[] kits_a, T[] kits_b)
        {
            T[] newKitArray = new T[kits_a.Length + kits_b.Length];
            kits_a.CopyTo(newKitArray, 0);
            kits_b.CopyTo(newKitArray, kits_a.Length);
            return newKitArray;
        }
        public static string GenerateUUID(string inputName)
        {
            // Convert the input name to a byte array
            byte[] nameBytes = Encoding.UTF8.GetBytes(inputName);
            // Create an instance of the MD5 algorithm
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the name bytes
                byte[] hashBytes = md5.ComputeHash(nameBytes);
                // Set the version and variant bits of the UUID
                hashBytes[6] = (byte)((hashBytes[6] & 0x0F) | (5 << 4)); // Version 5 (name-based UUID)
                hashBytes[8] = (byte)((hashBytes[8] & 0x3F) | 0x80); // Variant bits (RFC 4122)
                // Create a new UUID from the hash bytes
                return new Guid(hashBytes).ToString();
            }
        }
    }
}
