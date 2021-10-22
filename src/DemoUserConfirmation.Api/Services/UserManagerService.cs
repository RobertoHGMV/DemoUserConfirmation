using System.Threading;
using System.Threading.Tasks;
using DemoUserConfirmation.Api.Models;

namespace DemoUserConfirmation.Api.Services
{
    public class UserManagerService
    {
        public readonly DbTemp _dbTemp;

        public UserManagerService(DbTemp dbTemp)
        {
            _dbTemp = dbTemp;
        }

        public async Task CreateAsync(User user) 
        {
            var userManager = new UserManager
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            await _dbTemp.AddUserManagerAsync(userManager);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user) 
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(user.Id);
            var token = System.Convert.ToBase64String(bytes);

            var userManagerFromDb = await _dbTemp.GetUserManagerByIdAsync(user.Id);
            userManagerFromDb.Token = token;

            return token;
        }

        public async Task<bool> ConfirmEmailAsync(UserManager user, string token) 
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            return user.EmailConfirmed = user.Token == token;
        }

        public async Task<UserManager> FindByIdAsync(string email) 
        {
            return await _dbTemp.GetUserManagerByEmailAsync(email);
        }

        public async Task<UserManager> FindByEmailAsync(string email) 
        {
            return await _dbTemp.GetUserManagerByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserManager user) 
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(user.Id);
            var token = System.Convert.ToBase64String(bytes);

            var userManagerFromDb = await _dbTemp.GetUserManagerByIdAsync(user.Id);
            userManagerFromDb.Token = token;

            return token;
        }

        public async Task<bool> ResetPasswordAsync(UserManager user, string token, string password) 
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var newPassword = System.Convert.ToBase64String(bytes);
            user.Password = password;

            var userManagerFromDb = await _dbTemp.GetUserManagerByIdAsync(user.Id);
            userManagerFromDb.Token = token;

            return true;
        }
    }
}