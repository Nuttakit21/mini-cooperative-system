using System.IdentityModel.Tokens.Jwt;

namespace MiniCoop.Web.Helpers
{
    public static class JwtHelper
    {
        public static DateTime? GetExpiry(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.ValidTo;
        }
        public static bool IsExpired(string token)
        {
            var exp = GetExpiry(token);
            return exp == null || exp <= DateTime.UtcNow;
        }

    }
}