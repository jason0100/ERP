using API.Models.RSA;
using System;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class RSAHelper : IRSAHelper
    {
        public RSAKey GenerateKeys()
        {
            var keys = new RSAKey();
            var rsa = RSA.Create();
            //匯出公鑰
            var pubKey = rsa.ExportRSAPublicKey();

            //匯出私鑰
            var priKey = rsa.ExportRSAPrivateKey();

            keys.publicKey = Convert.ToBase64String(pubKey);
            keys.privateKey = Convert.ToBase64String(priKey);

            return keys;
        }

        public string SignData(string challenge, string privateKey)
        {
            var rsa = RSA.Create();
            var pk = privateKey;
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(pk), out int bytesreadPublic);
            var dataByte = Encoding.Unicode.GetBytes(challenge);
            var sign = rsa.SignData(dataByte, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1); //簽名
            return Convert.ToBase64String(sign);
        }



        public bool VerifySignature(string challenge, string sign, string publicKey)
        {

            var rsa = RSA.Create();
            var puk = publicKey;
            //載入key
            rsa.ImportRSAPublicKey(Convert.FromBase64String(puk), out int bytesreadPublic);
            var dataByte = Encoding.Unicode.GetBytes(challenge);
            var signByte = Convert.FromBase64String(sign);
            bool isSuccess = rsa.VerifyData(dataByte, signByte, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1); //驗簽

            return isSuccess;
        }

    }
}
