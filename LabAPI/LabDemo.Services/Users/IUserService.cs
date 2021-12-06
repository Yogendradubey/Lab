using LabDemo.Models;

namespace LabDemo.Services.Users
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        User GetUser(string userName, string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        string GenerateJWT(User user);
    }
}
