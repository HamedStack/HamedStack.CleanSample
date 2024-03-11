using Microsoft.AspNetCore.Identity;

namespace CleanSample.WebApi.Identity
{
    public class AdvIdentityUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
