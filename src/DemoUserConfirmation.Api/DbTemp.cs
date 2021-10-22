using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DemoUserConfirmation.Api.Models;

namespace DemoUserConfirmation.Api
{
    public class DbTemp
    {
        public List<UserManager> UserManagers { get; set; }
        public List<User> Users { get; set; }

        public DbTemp()
        {
            UserManagers = new List<UserManager>();
            Users = new List<User>();
        }

        public async Task AddUserAsync(User user)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            Users.Add(user);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            return Users.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddUserManagerAsync(UserManager userManager)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            UserManagers.Add(userManager);
        }

        public async Task<UserManager> GetUserManagerByIdAsync(string id)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            return UserManagers.FirstOrDefault(c => c.Id == id);
        }

        public async Task<UserManager> GetUserManagerByEmailAsync(string email)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(1));

            return UserManagers.FirstOrDefault(c => c.Email == email);
        }
    }
}