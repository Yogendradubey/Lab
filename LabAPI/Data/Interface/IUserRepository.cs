using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Interface
{
    public interface IUserRepository
    {
        public string CreateUser(string Username, string Password);
        public string AuthenticateUser(string Username, string Password);
    }
}
