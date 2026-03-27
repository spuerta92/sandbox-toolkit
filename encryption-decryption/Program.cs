using System.Security.Cryptography;

static class Program
{
    static byte[] AESEncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        byte[] encrypted;

        // Create an Aes object with the specified key and IV.
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            // Create a new MemoryStream object to contain the encrypted bytes.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a CryptoStream object to perform the encryption.
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt the plaintext.
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    encrypted = memoryStream.ToArray();
                }
            }
        }

        return encrypted;
    }

    static string AESDecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        string decrypted;

        // Create an Aes object with the specified key and IV.
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            // Create a new MemoryStream object to contain the decrypted bytes.
            using (MemoryStream memoryStream = new MemoryStream(cipherText))
            {
                // Create a CryptoStream object to perform the decryption.
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // Decrypt the ciphertext.
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        decrypted = streamReader.ReadToEnd();
                    }
                }
            }
        }

        return decrypted;
    }

    /// <summary>
    /// encryption / decryption algorithms overview (AES, RSA, DES, 3DES, MD5) using System.Security.Cryptography.
    /// Not creating algorithms from scratch.
    /// </summary>
    static void Main()
    {
        // https://www.c-sharpcorner.com/article/best-algorithm-for-encrypting-and-decrypting-a-string-in-c-sharp/
        // AES (Advanced Encryption Standard)
        // The Advanced Encryption Standard (AES), introduced by the National Institute of Standards and Technology (NIST) in 2001,
        // is a strong encryption algorithm derived from the Rijndael cipher family. AES employs the Rijndael block cipher to enhance
        // security with three distinct key sizes: 128, 192, and 256 bits. It operates as a symmetric block cipher, employing a single
        // key for encryption and decryption processes

        string original = "Hello my company employees";
        byte[] encrypted;
        string decrypted;

        using (Aes aes = Aes.Create())
        {
            encrypted = AESEncryptStringToBytes(original, aes.Key, aes.IV);
            Console.WriteLine("Encrypted: {0}", Convert.ToBase64String(encrypted));

            decrypted = AESDecryptStringFromBytes(encrypted, aes.Key, aes.IV);
            Console.WriteLine("Decrypted: {0}", decrypted);
        }

        // RSA

        // DES

        // 3DES

        // MD5
    }
}