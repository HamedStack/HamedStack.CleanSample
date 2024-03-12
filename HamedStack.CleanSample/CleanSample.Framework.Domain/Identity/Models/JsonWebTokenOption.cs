namespace CleanSample.Framework.Domain.Identity.Models;

public class JsonWebTokenOption
{
    public int RefreshTokenValidityInDays { get; set; } = 7;
    public int AccessTokenValidityInMinutes { get; set; } = 5;
    public bool RequireHttpsMetadata { get; set; } = true;
    public string SigningKey { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
}