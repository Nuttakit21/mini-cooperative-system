using System.Net;

namespace MiniCoop.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly CustomAuthStateProvider _auth;

        public ApiService(HttpClient http, CustomAuthStateProvider auth)
        {
            _http = http;
            _auth = auth;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await _http.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _auth.Logout(expired: true);
                return default;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> PostAsync<T>(string url, T data)
        {
            var response = await _http.PostAsJsonAsync(url, data);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _auth.Logout(expired: true);
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}