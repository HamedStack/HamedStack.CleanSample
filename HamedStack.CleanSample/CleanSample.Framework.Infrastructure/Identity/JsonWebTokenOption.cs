namespace CleanSample.Framework.Infrastructure.Identity;

public class JsonWebTokenOption
{
    public bool RequireHttpsMetadata { get; set; } = true;
    public string SigningKey { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
}