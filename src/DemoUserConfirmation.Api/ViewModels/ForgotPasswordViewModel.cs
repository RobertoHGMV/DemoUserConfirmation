using System.ComponentModel.DataAnnotations;

namespace DemoUserConfirmation.Api.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }   
    }
}