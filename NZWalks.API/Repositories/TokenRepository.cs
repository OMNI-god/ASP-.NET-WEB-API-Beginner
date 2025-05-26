using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configurationManager;

        public TokenRepository(IConfiguration configurationManager)
        {
            this.configurationManager = configurationManager;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims =new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            }.Concat(roles.AsEnumerable().Select(role => new Claim(ClaimTypes.Role, role))).ToList();

            var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(configurationManager["Jwt:Key"]));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configurationManager["Jwt:Issuer"], configurationManager["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
