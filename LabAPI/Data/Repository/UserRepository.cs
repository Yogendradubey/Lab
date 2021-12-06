using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LaboratoryDbContext _dbContext;
        private readonly string _key;

        public UserRepository(LaboratoryDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _key = config.GetValue<string>("Key");
        }

        #region User
        public string AuthenticateUser(string username, string password)
        {
            var user = _dbContext.UserCredential.FirstOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateUser(string username, string password)
        {
            try
            {
                Random id = new Random();
                using (var db = _dbContext)
                {
                    UserCredential _user = new UserCredential();
                    _user.Id = id.Next(100);
                    _user.UserName = username;
                    _user.Password = password;
                    db.UserCredential.Add(_user);
                    db.SaveChanges();
                    return ResponseMessage.CreatedSuccessfully.ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
