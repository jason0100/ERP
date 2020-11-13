
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.Collections.Specialized;
using System.Security.Cryptography;
using API.Models.Users;
using API.Models;
using API.Models.Admin;
using API.Models.Token;
using API.Data.Interface;

namespace API.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ErpDbContext _DBContext;

        private readonly IConfiguration _config;
        public AuthRepository(ErpDbContext DBContext, IConfiguration config)
        {
            _DBContext = DBContext;
            
            _config = config;
        }

        public async Task<LoginHuman> Login(int card_id, string username)
        {
            var loginHuman = new LoginHuman();
            //loginHuman.token = new TokenModel();

            //if (card_id != 0)
            //{
            //    var admin = await _DBContext.admin
            //     //.Include(c => c.role)
            //     .FirstOrDefaultAsync(x => x.card_id == card_id); //Get user from database.
            //    loginHuman.uuid = admin.admin_uuid;
            //    if (admin.type != null)
            //        loginHuman.type = Convert.ToInt32(admin.type);
            //}
            //else if (!string.IsNullOrEmpty(username))
            //{

            //    var admin = await _DBContext.admin
            //     //.Include(c => c.role)
            //     .FirstOrDefaultAsync(x => x.username == username); //Get user from database.

            //    loginHuman.uuid = admin.admin_uuid;
            //    if (admin.type != null)
            //        loginHuman.type = Convert.ToInt32(admin.type);
            //}


            ////===========generate token=================
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_config["TokenKey"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]{
            //        new Claim("uuid",loginHuman.uuid),
            //      new Claim("type",loginHuman.type.ToString())
            //        //new Claim(ClaimTypes.Role,admin.role_.RoleName)
            //    }),

            //    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_config["TokenExpireTime"])),
            //    // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            //};
            //loginHuman.token.token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return loginHuman;
        }


        public string GenerateUUID()
        {

            var UUID = Guid.NewGuid().ToString();
            return UUID;

        }

        public void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = Encoding.UTF8.GetBytes("2020eID");
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = Encoding.UTF8.GetBytes("2020eID");
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i])
                        return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }
    }
}
