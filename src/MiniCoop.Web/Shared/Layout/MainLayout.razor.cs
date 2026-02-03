using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MiniCoop.Web.State;

namespace MiniCoop.Web.Shared.Layout
{
    public partial class MainLayout : IDisposable
    {
        [Inject] public AppState AppState { get; set; } = null!;
        [Inject] public NavigationManager Nav { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;

        bool isSidebarExpanded = false;
        bool _restored = false;

        protected override void OnInitialized()
        {
            AppState.OnMenusChanged += () => _ = OnMenusChangedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender || _restored)
                return;

            _restored = true;

            var app = await JS.InvokeAsync<string?>("appState.get", "currentApp");
            if (!string.IsNullOrWhiteSpace(app))
            {
                AppState.RestoreApp(app);
            }

            var sidebarObj = await JS.InvokeAsync<object>("appState.get", "sidebarExpanded");
            isSidebarExpanded = sidebarObj is bool b && b;

            StateHasChanged();
        }

        private async Task OnMenusChangedAsync()
        {
            if (AppState.Menus.Any())
            {
                isSidebarExpanded = true;
                await JS.InvokeVoidAsync("appState.set", "sidebarExpanded", true);
            }

            await InvokeAsync(StateHasChanged);
        }

        async Task ToggleSidebar()
        {
            if (!AppState.Menus.Any())
                return;

            isSidebarExpanded = !isSidebarExpanded;
            await JS.InvokeVoidAsync("appState.set", "sidebarExpanded", isSidebarExpanded);
        }

        async Task Home()
        {
            AppState.ResetApp();

            await JS.InvokeVoidAsync("appState.remove", "currentApp");
            await JS.InvokeVoidAsync("appState.remove", "sidebarExpanded");

            Nav.NavigateTo("/", true);
        }

        string SidebarStyle => isSidebarExpanded ? "width:260px; transition:width .25s ease;" : "width:0; overflow:hidden; transition:width .25s ease;";

        public void Dispose()
        {
            AppState.OnMenusChanged -= () => _ = OnMenusChangedAsync();
        }
    }
}