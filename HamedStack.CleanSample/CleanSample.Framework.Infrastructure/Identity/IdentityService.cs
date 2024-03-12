using CleanSample.Framework.Domain.Identity;
using CleanSample.Framework.Domain.Identity.Models;
using CleanSample.Framework.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace CleanSample.Framework.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly JsonWebTokenOption _jwtOption;
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityService(UserManager<ApplicationUser> userManager, JsonWebTokenOption jwtOption)
    {
        _userManager = userManager;
        _jwtOption = jwtOption;
    }
    public Task<TokenModel?> Login(LoginModel model)
    {
        throw new NotImplementedException();
    }

    public Task<TokenModel?> GetRefreshToken(TokenModel tokenModel)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Register(RegisterModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            return new Result(ResultStatus.Failure, "UserName is required.");
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return new Result(ResultStatus.Failure, "Password is required.");
        }
        if (string.IsNullOrWhiteSpace(model.Email))
        {
            return new Result(ResultStatus.Failure, "Email is required.");
        }
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
            return new Result(ResultStatus.Failure, "User already exists!");
        }
        var appUser = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.UserName,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(appUser, model.Password);
        if (result.Succeeded)
            return new Result(ResultStatus.Success);
        var error = result.Errors.Select(e => e.Description).Aggregate((a, b) => a + Environment.NewLine + b);
        return new Result(ResultStatus.Failure, error);
    }

    public Task<bool> Revoke(string userName)
    {
        throw new NotImplementedException();
    }

    public Task RevokeAll()
    {
        throw new NotImplementedException();
    }
}