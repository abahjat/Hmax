using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Hmax
{
    public class EncryptionUtil
    {
        private readonly X509Certificate2 cert;
        //private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private readonly RSACryptoServiceProvider rngCsp;
        private byte[] keyAndIvBytes = new byte[16];
        //This constructor is added for those who wants to experiement with the tool without actual certificate
        public EncryptionUtil()
        {
            // Access Personal (MY) certificate store of current user
            var my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            if (my.Certificates.Count > 0)
            {
                cert = my.Certificates[0];
                if (cert.PrivateKey != null)
                    rngCsp = (RSACryptoServiceProvider) cert.PrivateKey;
                else
                    rngCsp = new RSACryptoServiceProvider(1024);
            }
            else
            {
                cert = new X509Certificate2("dummyCert1.cert");
                if (cert.PrivateKey != null)
                    rngCsp = (RSACryptoServiceProvider) cert.PrivateKey;
                else
                    rngCsp = new RSACryptoServiceProvider(1024);
            }
        }

        public EncryptionUtil(string subject)
        {
            // Access Personal (MY) certificate store of current user
            var my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            foreach (var certTemp in my.Certificates)
            {
                if (certTemp.Subject.Contains(subject))
                {
                    //Return Cert with subject 
                    rngCsp = (RSACryptoServiceProvider) certTemp.PrivateKey;
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
            rngCsp = (RSACryptoServiceProvider) cert.PrivateKey;
        }

        public string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public string EncryptAndEncode(string plaintext)
        {
            return ByteArrayToHexString(AesEncrypt(plaintext));
        }

        public char encryptChar(string inputText)
        {
            var resultvalue = 0;
            var longValue = AesEncrypt(inputText);
            foreach (var v in longValue)
            {
                resultvalue += (v%int.MaxValue);
            }
            return (char) (resultvalue%122);
        }

        public byte[] AesEncrypt(string inputText)
        {
            var inputBytes = Encoding.UTF8.GetBytes(inputText); //AbHLlc5uLone0D1q
            //return rngCsp.Encrypt(inputBytes,false); //updated to RSA now
            var blobSeed = rngCsp.ExportCspBlob(false);
            var blob = rngCsp.SignData(blobSeed, new SHA1CryptoServiceProvider());
            byte[] result = null;
            var rgbIV = Encoding.UTF8.GetBytes("tR7Nr6wZbXjYMCuVaAGWNLIO");
            Console.WriteLine(blob.Length);

            using (var memoryStream = new MemoryStream())
            {
                using (
                    var cryptoStream = new CryptoStream(memoryStream,
                        GetCryptoAlgorithm().CreateEncryptor(convertKey(blob, 192, rgbIV), rgbIV),
                        CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    result = memoryStream.ToArray();
                }
            }

            return result;
        }

        public byte[] convertKey(byte[] key, int toSize, byte[] IV)
        {
            var keyString = ByteToString(key);
            var objPass = new PasswordDeriveBytes(keyString, IV);
            return objPass.CryptDeriveKey("TripleDES", "SHA1", toSize, new byte[8]);
        }

        private static RijndaelManaged GetCryptoAlgorithm()
        {
            var algorithm = new RijndaelManaged();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 192;
            algorithm.BlockSize = 192;
            return algorithm;
        }

        public string ByteToString(byte[] buff)
        {
            var sbinary = "";

            for (var i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        /*Generates a random Number for key*/

        public static int GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random();
            return Convert.ToInt32(random.NextDouble()*(maximum - minimum) + minimum);
        }

        public string getHMAC5(string message)
        {
            //key = GetRandomNumber(3000, 4000);

            var encoding = new ASCIIEncoding();

            //byte[] keyByte = cert.Export(X509ContentType.Pkcs12); //keyAndIvBytes;// = UTF8Encoding.UTF8.GetBytes("tR7nR6wZbGjYMCuV"); //encoding.GetBytes(key);

            //HMACSHA512 hmacsha512 = new HMACSHA512(keyByte);
            var sha1 = new SHA1Managed();

            var messageBytes = encoding.GetBytes(message);


            var hashmessage = sha1.ComputeHash(messageBytes); //hmacsha512.ComputeHash(messageBytes);

            var signedBytes = rngCsp.SignHash(hashmessage, CryptoConfig.MapNameToOID("SHA1"));
            var hmac5 = ByteToString(signedBytes);

            return hmac5;
        }
    }
}