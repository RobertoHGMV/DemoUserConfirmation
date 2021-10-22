using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DemoUserConfirmation.Api.Models;
using DemoUserConfirmation.Api.ViewModels;

namespace DemoUserConfirmation.Api.Services
{
    public class UserService
    {
        DbTemp _dbTemp;

        public UserService(DbTemp dbTemp)
        {
            _dbTemp = dbTemp;
        }

        public async Task<User> AddAsync(UserManager userManager)
        {
            var user = new User(userManager.Name, userManager.Email, userManager.Password);
            await _dbTemp.AddUserAsync(user);
            return user;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbTemp.GetUserByIdAsync(id);
        }
    }
}