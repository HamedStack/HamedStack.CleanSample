namespace CleanSample.Framework.Domain.Identity.Models;

public class TokenModel
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}