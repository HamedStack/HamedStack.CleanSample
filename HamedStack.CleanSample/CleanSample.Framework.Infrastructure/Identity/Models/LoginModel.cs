using System.ComponentModel.DataAnnotations;

namespace CleanSample.Framework.Infrastructure.Identity.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    public required string Username { get; set; }
}