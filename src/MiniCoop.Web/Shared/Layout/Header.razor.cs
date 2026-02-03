using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using MiniCoop.Web.Services;
using MiniCoop.Web.State;
using Radzen;
using Radzen.Blazor;
using System.Security.Claims;

namespace MiniCoop.Web.Shared.Layout
{
    public partial class Header : IDisposable
    {
        [Parameter] public EventCallback OnToggle { get; set; }
        [Parameter] public EventCallback OnHome { get; set; }

        [Inject] AppState AppState { get; set; } = null!;
        [Inject] ProtectedLocalStorage LocalStorage { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthState { get; set; } = default!;
        [Inject] CustomAuthStateProvider CustomAuthState { get; set; } = default!;
        [Inject] NavigationManager Nav { get; set; } = default!;
        [Inject] NotificationService Notification { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = null!;

        protected string User_name = "";
        protected string Role = "";

        protected override void OnInitialized()
        {
            AuthState.AuthenticationStateChanged += OnAuthStateChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            var authState = await AuthState.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                User_name = user.Identity?.Name ?? "";
                Role = user.FindFirstValue(ClaimTypes.Role) ?? "";
                StateHasChanged();
            }

            var reason = await LocalStorage.GetAsync<string>("Session");
            if (reason.Success && reason.Value == "Expired")
            {
                Notification.Notify(NotificationSeverity.Warning, "Session หมดอายุ", "กรุณาเข้าสู่ระบบใหม่");
                await LocalStorage.DeleteAsync("Session");
            }
        }

        private async void OnAuthStateChanged(Task<AuthenticationState> task)
        {
            var authState = await task;
            var user = authState.User;

            User_name = user.Identity?.IsAuthenticated == true ? user.Identity?.Name ?? "" : "";

            await InvokeAsync(StateHasChanged);
        }

        async Task ToggleSidebar()
        {
            await OnToggle.InvokeAsync();
        }

        async Task Home()
        {
            await OnHome.InvokeAsync();
        }

        public async Task MenuClick(RadzenProfileMenuItem item)
        {
            if (item.Value?.ToString() == "LogOff")
            {
                await LogOff();
            }
        }

        public async Task LogOff()
        {
            AppState.ResetApp();

            await JS.InvokeVoidAsync("appState.clear");

            await CustomAuthState.Logout();            

            Nav.NavigateTo("/login", true);
        }

        public void Dispose()
        {
            AuthState.AuthenticationStateChanged -= OnAuthStateChanged;
        }
    }
}