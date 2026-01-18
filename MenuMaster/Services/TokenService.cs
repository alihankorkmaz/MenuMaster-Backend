using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MenuMaster.Models;
using MenuMaster.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
    }

    // for RESTAURANT tokens
    public string GenerateToken(Restaurant restaurant)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, restaurant.Name),
            new Claim("RestaurantId", restaurant.Id.ToString()),
            new Claim(ClaimTypes.Role, "Restaurant"),
            new Claim(ClaimTypes.Email, restaurant.Email)
        };
        return CreateToken(claims);
    }

    // for USER tokens
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name ?? user.Username),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Email, user.Email)
        };
        return CreateToken(claims);
    }

    private string CreateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}