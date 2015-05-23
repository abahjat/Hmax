using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Hmax
{
    public class EncryptionUtil
    {
        private byte[] keyAndIvBytes = new byte[16];
        //private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        RSACryptoServiceProvider rngCsp = null;
        private X509Certificate2 cert =null;

        public EncryptionUtil()
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);
            string secret = "asdwejDLKFJAasdfasfasfertwtlaksdjf43fsflasdsadsfa";
            byte[] secretData = Encoding.UTF8.GetBytes(secret);

            // Find the certificate we'll use to sign            
            if (my.Certificates.Count > 0)
            {
                cert = my.Certificates[0];
                if (cert.PrivateKey != null)
                    rngCsp = (RSACryptoServiceProvider)cert.PrivateKey;
                else
                    rngCsp = new RSACryptoServiceProvider(1024);

            }else
            {
                cert = new X509Certificate2("dummyCert1.cert");
                if (cert.PrivateKey != null)
                    rngCsp = (RSACryptoServiceProvider)cert.PrivateKey;
                else
                    rngCsp = new RSACryptoServiceProvider(1024);
                //cert = this.
            }
        }

        public EncryptionUtil(string subject)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 certTemp in my.Certificates)
            {
                if (certTemp.Subject.Contains(subject))
                {
                    //Return Cert with subject 
                    rngCsp = (RSACryptoServiceProvider)certTemp.PrivateKey;
                    cert = certTemp;
                    break;
                }
            }
            if (rngCsp == null)
            {
                throw new Exception("No valid cert was found");
            }
            
        }

        public EncryptionUtil(string subject, string path)
        {
            cert = new X509Certificate2(path);
            rngCsp = (RSACryptoServiceProvider)cert.PrivateKey;

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

 

        public string EncryptAndEncode(string plaintext)
        {
            return ByteArrayToHexString(AesEncrypt(plaintext));
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
            //return rngCsp.Encrypt(inputBytes,false); //updated to RSA now
            byte[] blobSeed = rngCsp.ExportCspBlob(includePrivateParameters: false);
            byte[] blob = rngCsp.SignData(blobSeed, new SHA1CryptoServiceProvider());
            byte[] result = null;
            byte[] rgbIV = UTF8Encoding.UTF8.GetBytes("tR7Nr6wZbXjYMCuVaAGWNLIO");
            Console.WriteLine(blob.Length);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetCryptoAlgorithm().CreateEncryptor(convertKey(blob,192,rgbIV),rgbIV), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    result = memoryStream.ToArray();
                }
            }
            
            return result;
             
        }

        public byte[] convertKey(byte[] key, int toSize,byte[] IV)
        {
            string keyString = ByteToString(key);
            PasswordDeriveBytes objPass = new PasswordDeriveBytes(keyString,IV);
            return objPass.CryptDeriveKey("TripleDES", "SHA1", toSize, new byte[8]);
        }


        private static RijndaelManaged GetCryptoAlgorithm()
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 192;
            algorithm.BlockSize = 192;
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

        
        public string getHMAC5(string message)
        {

            //key = GetRandomNumber(3000, 4000);
            
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            //byte[] keyByte = cert.Export(X509ContentType.Pkcs12); //keyAndIvBytes;// = UTF8Encoding.UTF8.GetBytes("tR7nR6wZbGjYMCuV"); //encoding.GetBytes(key);

            //HMACSHA512 hmacsha512 = new HMACSHA512(keyByte);
            SHA1Managed sha1 = new SHA1Managed();
            
            byte[] messageBytes = encoding.GetBytes(message);


            byte[] hashmessage = sha1.ComputeHash(messageBytes);//hmacsha512.ComputeHash(messageBytes);
            
            byte[] signedBytes = rngCsp.SignHash(hashmessage, CryptoConfig.MapNameToOID("SHA1"));
            string hmac5 = ByteToString(signedBytes);

            return hmac5;

        }

        
    }
}
