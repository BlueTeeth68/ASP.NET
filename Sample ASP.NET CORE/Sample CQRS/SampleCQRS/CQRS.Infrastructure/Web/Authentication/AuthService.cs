using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CQRS.Application.Features.Users.Commands.Requests;
using CQRS.Application.Features.Users.DTOs;
using CQRS.Application.Identity;
using CQRS.Domain.Exceptions;
using CQRS.Infrastructure.Configurations;
using CQRS.Persistence.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CQRS.Infrastructure.Web.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    #region constructor

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    #endregion


    #region Services

    public async Task<LoginResultDto> Login(LoginCommand request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new NotFoundException("User NotFound");
        }

        var result =
            await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new NotFoundException("Incorrect password.");
        }

        var accessToken = await GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshToken(user);

        var response = new LoginResultDto()
        {
            Id = user.Id,
            Email = user.Email ?? "",
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return response;
    }

    #endregion

    #region Private methods

    private async Task<string> GenerateAccessToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(t => new Claim(ClaimTypes.Role, t)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpiredInDays),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task<string> GenerateRefreshToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshExpiredInDays),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    #endregion
}