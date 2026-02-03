using Microsoft.AspNetCore.Components;
using MiniCoop.Application.DTOs.Menu;
using MiniCoop.Web.Services;
using MiniCoop.Web.State;

namespace MiniCoop.Web.Shared.Layout
{
    public partial class Sidebar : IDisposable
    {
        [Inject] public AppState AppState { get; set; } = null!;
        [Inject] public ApiService Api { get; set; } = null!;

        [Parameter] public bool Collapsed { get; set; }

        protected override void OnInitialized()
        {
            AppState.OnAppChanged += LoadMenus;
            if (!string.IsNullOrWhiteSpace(AppState.CurrentApp))
            {
                LoadMenus();
            }
        }

        private async void LoadMenus()
        {
            if (string.IsNullOrWhiteSpace(AppState.CurrentApp))
                return;

            var menus = await Api.GetAsync<List<MenuGroupDto>>($"api/menu/{AppState.CurrentApp}");

            if (menus != null)
                AppState.SetMenus(menus);

            await InvokeAsync(StateHasChanged);
        }

        private string activeGroupName = "";

        private void ToggleGroup(string groupName)
        {
            // ถ้า Sidebar หดอยู่ ไม่ต้องทำ Accordion (ให้โชว์แค่ไอคอน)
            if (Collapsed) return;

            if (activeGroupName == groupName)
                activeGroupName = ""; // กดซ้ำเพื่อปิด
            else
                activeGroupName = groupName; // เปิดกลุ่มใหม่ ปิดกลุ่มเก่าอัตโนมัติ
        }

        public void Dispose()
        {
            AppState.OnAppChanged -= LoadMenus;
        }
    }
}