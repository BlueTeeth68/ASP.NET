using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models.Users;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class CustomAuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;

    public CustomAuthController(IConfiguration config, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        this.config = config;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }
    
     [HttpPost]
        public async Task<IResult> Login([FromBody] LoginRequestDto loginRequestModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginRequestModel.Email, loginRequestModel.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var issuer = config["Jwt:Issuer"];
                var audience = config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, loginRequestModel.Email),
                        new Claim(JwtRegisteredClaimNames.Email, loginRequestModel.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                var identityUser = await userManager.FindByEmailAsync(loginRequestModel.Email);
                var roles = await userManager.GetRolesAsync(identityUser);

                foreach (var role in roles)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                // generate token with the above information
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return Results.Ok(stringToken);
            }

            return Results.Unauthorized();
        }
}