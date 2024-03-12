using CleanSample.Framework.Domain.Identity;
using CleanSample.Framework.Domain.Identity.Models;
using CleanSample.Framework.Domain.Results;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CleanSample.Framework.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JsonWebTokenOption _jwtOption;

    public IdentityService(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService, JsonWebTokenOption jwtOption)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _jwtOption = jwtOption;
    }
    public async Task<TokenModel?> Login(LoginModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            throw new ArgumentException("UserName is required.", nameof(model.UserName));
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            throw new ArgumentException("Password is required.", nameof(model.Password));
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            foreach (var userClaim in userClaims)
            {
                authClaims.Add(new Claim(userClaim.Type, userClaim.Value));
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(authClaims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOption.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        return null;
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

    public async Task<bool> Revoke(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return false;

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = new DateTime(1, 1, 1);

        await _userManager.UpdateAsync(user);

        return true;
    }
    public async Task Revoke(IEnumerable<string> userNames)
    {
        var tasks = new List<Task>();
        foreach (var userName in userNames)
        {
            tasks.Add(Revoke(userName));
        }
        await Task.WhenAll(tasks);
    }
    public async Task RevokeAll()
    {
        var tasks = new List<Task>();
        foreach (var user in _userManager.Users.ToList())
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = new DateTime(1, 1, 1);
            tasks.Add(_userManager.UpdateAsync(user));
        }
        await Task.WhenAll(tasks);
    }
}