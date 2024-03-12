using CleanSample.Framework.Domain.Identity.Models;
using CleanSample.Framework.Domain.Results;

namespace CleanSample.Framework.Domain.Identity;

public interface IIdentityService
{
    Task<TokenModel?> Login(LoginModel model);
    Task<TokenModel?> GetRefreshToken(TokenModel tokenModel);
    Task<Result> Register(RegisterModel model);
    Task<bool> Revoke(string userName);
    Task RevokeAll();
}