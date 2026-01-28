using System.Security.Claims;
using System.Text.Json;

namespace MiniCoop.Web.Services
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = Convert.FromBase64String(Pad(payload));
            var keyValuePairs =
                JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs!.Select(kvp =>
                new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private static string Pad(string base64)
        {
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }
    }
}