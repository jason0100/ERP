
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Admin;
using API.Models.Users;

namespace API.Data.Interface
{
    public interface IAuthRepository
    {
     
        Task<LoginHuman> Login(int card_id, string username);
   
        string GenerateUUID();
        void CreatePasswordHash(string password, out byte[] passwordHash);
        bool VerifyPassword(string password, byte[]  PasswordHash);
    }
}
