using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MiniCoop.Web.Helpers;
using System.Security.Claims;

namespace MiniCoop.Web.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _storage;
        private readonly TokenStore _store;

        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedLocalStorage storage, TokenStore store)
        {
            _storage = storage;
            _store = store;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_store.Token == null)
            {
                var result = await _storage.GetAsync<string>("jwt");
                _store.Token = result.Success ? result.Value : null;
            }

            if (string.IsNullOrWhiteSpace(_store.Token))
                return new AuthenticationState(_anonymous);

            if (JwtHelper.IsExpired(_store.Token))
            {
                await Logout(expired: true);
                return new AuthenticationState(_anonymous);
            }

            var claims = JwtParser.ParseClaimsFromJwt(_store.Token);
            var identity = new ClaimsIdentity(claims, "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task SetToken(string token)
        {
            _store.Token = token;
            await _storage.SetAsync("jwt", token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout(bool expired = false)
        {
            _store.Token = null;

            await _storage.DeleteAsync("jwt");

            if (expired)
                await _storage.SetAsync("Session", "Expired");

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous))
            );
        }
    }
}