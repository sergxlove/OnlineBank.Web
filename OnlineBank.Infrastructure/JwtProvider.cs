using Microsoft.IdentityModel.Tokens;
using OnlineBank.Infrastructure.Abstractions;
using OnlineBank.Infrastructure.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OnlineBank.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        public string? GenerateToken(JwtRequest request)
        {
            JwtSecurityToken jwt = new(
                    issuer: request.Issuer,
                    audience: request.Audience,
                    claims: request.Claims,
                    expires: request.Expires,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(request.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
