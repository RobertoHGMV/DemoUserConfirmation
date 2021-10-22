namespace DemoUserConfirmation.Api.Models
{
    public class UserManager
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}