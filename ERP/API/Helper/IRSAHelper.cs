using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.RSA
{
    public interface IRSAHelper
    {
        RSAKey GenerateKeys();
        string SignData(string challenge, string privateKey);
        bool VerifySignature(string challenge, string sign, string publicKey);
    }
}
