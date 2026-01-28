using Microsoft.AspNetCore.Components.Authorization;
using MiniCoop.Web.Models.Auth;
using MiniCoop.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly CustomAuthStateProvider _auth;

    public AuthService(HttpClient http, AuthenticationStateProvider auth)
    {
        _http = http;
        _auth = (CustomAuthStateProvider)auth;
    }

    public async Task<(bool success, string? error)> Login(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);

        if (!response.IsSuccessStatusCode)
            return (false, "เข้าสู่ระบบไม่สำเร็จ");

        var result =
            await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result == null)
            return (false, "ไม่พบ token");

        await _auth.SetToken(result.Token);

        return (true, null);
    }
}