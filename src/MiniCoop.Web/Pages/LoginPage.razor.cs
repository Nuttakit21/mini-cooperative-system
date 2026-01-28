using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MiniCoop.Web.Models.Auth;
using Radzen;

namespace MiniCoop.Web.Pages
{
    public partial class LoginPage
    {
        [Inject] ProtectedLocalStorage LocalStorage { get; set; } = default!;
        [Inject] AuthService AuthService { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthState { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] NotificationService Notification { get; set; } = default!;

        [SupplyParameterFromQuery]
        public string? returnUrl { get; set; }

        protected LoginRequest Model = new();
        protected bool IsLoading;
        protected string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            if (state.User.Identity?.IsAuthenticated == true)
            {
                Navigation.NavigateTo("/", true);
                return;
            }

            var reason = await LocalStorage.GetAsync<string>("Session");
            if (reason.Success && reason.Value == "Expired")
            {
                Notification.Notify(NotificationSeverity.Warning, "Session หมดอายุ", "กรุณาเข้าสู่ระบบใหม่");

                await LocalStorage.DeleteAsync("Session");
            }
        }
        protected async Task OnLogin()
        {
            IsLoading = true;
            ErrorMessage = "";

            if (string.IsNullOrWhiteSpace(Model.Username))
            {
                ErrorMessage = "กรุณากรอกชื่อผู้ใช้งาน";
                IsLoading = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Model.Password))
            {
                ErrorMessage = "กรุณากรอกรหัสผ่าน";
                IsLoading = false;
                return;
            }

            var (success, error) = await AuthService.Login(Model);

            IsLoading = false;

            if (success)
            {
                Navigation.NavigateTo(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl, true);
            }
            else
            {
                ErrorMessage = error ?? "ไม่สามารถเข้าสู่ระบบได้";
            }
        }
    }
}