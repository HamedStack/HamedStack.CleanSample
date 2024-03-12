using CleanSample.Domain.Identity.Models;
using CleanSample.Domain.Results;

namespace CleanSample.Domain.Identity;

public interface IIdentityService
{
    Task<TokenModel?> Login(LoginModel model);
    Task<TokenModel?> GetRefreshToken(TokenModel tokenModel);
    Task<Result> Register(RegisterModel model);
    Task<bool> Revoke(string userName);
    Task RevokeAll();
}