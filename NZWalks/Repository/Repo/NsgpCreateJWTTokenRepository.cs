using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.Repository.Repo;

public class NsgpCreateJWTTokenRepository : ICreateTokenRepository
{
    private readonly IConfiguration _configuration;

    public NsgpCreateJWTTokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        //create claims
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Email, user.Email)); 
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        //create key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //create Credientials
        var credientials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Create Token
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires:DateTime.UtcNow.AddMonths(14),
            signingCredentials:credientials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
