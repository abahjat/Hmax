using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Numerics;

namespace Hmac
{
    public class EncryptionUtil
    {
        private byte[] keyAndIvBytes = new byte[16];
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public EncryptionUtil()
        {
            // You'll need a more secure way of storing this, I hope this isn't
            // the real key
            //keyAndIvBytes = UTF8Encoding.UTF8.GetBytes("tR7nR6wZHGjYMCuV");

            rngCsp.GetBytes(keyAndIvBytes);
        }

        public EncryptionUtil(string key)
        {
            // You'll need a more secure way of storing this, I hope this isn't
            // the real key
            keyAndIvBytes = UTF8Encoding.UTF8.GetBytes(key);
        }

        public string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public string DecodeAndDecrypt(string cipherText)
        {
            string DecodeAndDecrypt = AesDecrypt(StringToByteArray(cipherText));
            return (DecodeAndDecrypt);
        }

        public string EncryptAndEncode(string plaintext)
        {
            return ByteArrayToHexString(AesEncrypt(plaintext));
        }

        public string AesDecrypt(Byte[] inputBytes)
        {
            Byte[] outputBytes = inputBytes;

            string plaintext = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream(outputBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetCryptoAlgorithm().CreateDecryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        public char encryptChar(string inputText)
        {

            int resultvalue = 0;
            byte[] longValue = AesEncrypt(inputText);
            foreach (byte v in longValue)
            {
                resultvalue += (((int)v) % int.MaxValue);
            }
            return (char)(resultvalue % 122);
        }

        public byte[] AesEncrypt(string inputText)
        {
            byte[] inputBytes = UTF8Encoding.UTF8.GetBytes(inputText);//AbHLlc5uLone0D1q

            byte[] result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetCryptoAlgorithm().CreateEncryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    result = memoryStream.ToArray();
                }
            }

            return result;
        }


        private static RijndaelManaged GetCryptoAlgorithm()
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            return algorithm;
        }

        public string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        /*Generates a random Number for key*/
        public static int GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return Convert.ToInt32(random.NextDouble() * (maximum - minimum) + minimum);
        }

        public string getHMAC(string message, int key)
        {

            key = GetRandomNumber(3000, 4000);

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = keyAndIvBytes;//= encoding.GetBytes(key); //UTF8Encoding.UTF8.GetBytes("tR7nR6wZHGjYMCuV");

            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            HMACSHA384 hmacsha384 = new HMACSHA384(keyByte);
            HMACSHA512 hmacsha512 = new HMACSHA512(keyByte);

            byte[] messageBytes = encoding.GetBytes(message);

            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);

            string hmac1 = ByteToString(hashmessage);

            hashmessage = hmacsha1.ComputeHash(messageBytes);

            string hmac2 = ByteToString(hashmessage);

            hashmessage = hmacsha256.ComputeHash(messageBytes);

            string hmac3 = ByteToString(hashmessage);

            hashmessage = hmacsha384.ComputeHash(messageBytes);

            string hmac4 = ByteToString(hashmessage);

            hashmessage = hmacsha512.ComputeHash(messageBytes);

            string hmac5 = ByteToString(hashmessage);

            return hmac5;

        }

        public string getHMAC5(string message)
        {

            //key = GetRandomNumber(3000, 4000);

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = keyAndIvBytes;// = UTF8Encoding.UTF8.GetBytes("tR7nR6wZbGjYMCuV"); //encoding.GetBytes(key);

            HMACSHA512 hmacsha512 = new HMACSHA512(keyByte);

            byte[] messageBytes = encoding.GetBytes(message);

            byte[] hashmessage = hmacsha512.ComputeHash(messageBytes);

            string hmac5 = ByteToString(hashmessage);

            return hmac5;

        }
    }
}
