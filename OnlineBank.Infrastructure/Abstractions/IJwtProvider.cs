using OnlineBank.Infrastructure.Contracts;

namespace OnlineBank.Infrastructure.Abstractions
{
    public interface IJwtProvider
    {
        string? GenerateToken(JwtRequest request);
    }
}
