using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopMate.BLL.Service.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopMate.BLL.Service.Implementation
{
    public class TokenServiceImp : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenServiceImp(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(IList<Claim> claims)
        {
            var securitykeystring = _configuration.GetSection("SecretKey").Value;
            var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring!);
            SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expire = DateTime.UtcNow.AddDays(1);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: signingCredentials);


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}


