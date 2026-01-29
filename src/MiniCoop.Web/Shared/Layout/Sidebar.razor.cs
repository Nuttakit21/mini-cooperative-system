using Microsoft.AspNetCore.Components;
using MiniCoop.Web.Services;

namespace MiniCoop.Web.Shared.Layout
{
    public partial class Sidebar
    {
        [Parameter] public bool Collapsed { get; set; }
        [Inject] public ApiService ApiService { get; set; } = null!;

        protected List<MenuDto> Menus = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await ApiService.GetAsync<List<MenuDto>>("api/menus/by-role");

            if (result != null)
            {
                Menus = result;
            }
        }
    }
}