using MiniCoop.Application.DTOs.Menu;

namespace MiniCoop.Web.State
{
    public class AppState
    {
        public string? CurrentApp { get; private set; }

        public IReadOnlyList<MenuGroupDto> Menus => _menus;
        private List<MenuGroupDto> _menus = new();

        public event Action? OnAppChanged;
        public event Action? OnMenusChanged;

        public void SetApp(string app)
        {
            if (string.IsNullOrWhiteSpace(app))
                return;

            if (CurrentApp == app)
                return;

            CurrentApp = app;
            _menus.Clear();

            OnAppChanged?.Invoke();
        }

        public void SetMenus(List<MenuGroupDto>? menus)
        {
            _menus = menus ?? new();
            OnMenusChanged?.Invoke();
        }

        public void RestoreApp(string app)
        {
            if (string.IsNullOrWhiteSpace(app))
                return;

            CurrentApp = app;
            _menus.Clear();

            OnAppChanged?.Invoke();
        }

        public void ResetApp()
        {
            if (CurrentApp == null && !_menus.Any())
                return;

            CurrentApp = null;
            _menus.Clear();

            OnAppChanged?.Invoke();
            OnMenusChanged?.Invoke();
        }
    }
}