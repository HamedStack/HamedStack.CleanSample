using System.ComponentModel.DataAnnotations;

namespace CleanSample.WebApi.Identity
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; }
    }
}
