using System.ComponentModel.DataAnnotations;

namespace CleanSample.Framework.Domain.Identity.Models;

public class RegisterModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    public required string UserName { get; set; }
}