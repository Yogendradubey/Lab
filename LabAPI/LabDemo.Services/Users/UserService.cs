using AutoMapper;
using LabDemo.DataProvider;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace LabDemo.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(ApplicationDbContext context,
            IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;

        }

        public void AddUser(Models.User User)
        {
            var UserEntity = _mapper.Map<User>(User);
            _context.Users.Add(UserEntity);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var User = _context.Users.Find(id);
            if (User != null)
            {
                _context.Users.Remove(User);
                _context.SaveChanges();
            }
        }

        public string GenerateJWT(Models.User user)
        {
            string response = string.Empty;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuth:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //claim is used to add identity to JWT token
                var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim("roles", user.Role),
                 new Claim("Date", DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

                var token = new JwtSecurityToken(Convert.ToString(_config["JwtAuth:Issuer"]),
                  _config["JwtAuth:Issuer"],
                  claims,    //null original value
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);
            response = new JwtSecurityTokenHandler().WriteToken(token).ToString(); ; //return access token
            return response;

        }

        public Models.User GetUser(int id)
        {
            var User = _context.Users.Find(id);
            if (User != null)
            {
                return _mapper.Map<Models.User>(User);
            }
            return null ;
        }

        public Models.User GetUser(string userName, string password)
        {
            var User = _context.Users.FirstOrDefault(u=>u.UserName.Equals(userName)&& u.Password.Equals(password));
            if (User != null)
            {
                return _mapper.Map<Models.User>(User);
            }
            return null;
        }

        public List<Models.User> GetUsers()
        {
            return _mapper.Map<List<Models.User>>(_context.Users.ToList());
        }

        public void UpdateUser(Models.User User)
        {
            var UserEntity = _mapper.Map<User>(User);
            _context.Users.Update(UserEntity);
            _context.SaveChanges();
        }
    }
}
