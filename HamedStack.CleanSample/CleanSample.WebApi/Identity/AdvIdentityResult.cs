using Microsoft.AspNetCore.Identity;

namespace CleanSample.WebApi.Identity
{
    public class AdvIdentityResult : IdentityResult
    {
        public new IEnumerable<IdentityError>? Errors { get; set; }
        public new bool Succeeded { get; set; }
    }
}
