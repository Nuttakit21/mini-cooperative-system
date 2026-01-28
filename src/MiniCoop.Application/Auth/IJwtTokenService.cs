namespace MiniCoop.Application.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(int userId, string username, string role);
    }
}