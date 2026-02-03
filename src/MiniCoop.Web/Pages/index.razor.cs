using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MiniCoop.Application.DTOs;
using MiniCoop.Web.Services;
using MiniCoop.Web.State;

namespace MiniCoop.Web.Pages;

public partial class Index
{
    [Inject] public ApiService Api { get; set; } = null!;
    [Inject] public NavigationManager Nav { get; set; } = null!;
    [Inject] public AppState AppState { get; set; } = null!;
    [Inject] IJSRuntime JS { get; set; } = null!;

    private List<ApplicationDto> _apps = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _apps = await Api.GetAsync<List<ApplicationDto>>("api/applications");
        }
        catch (Exception ex) 
        {

        }
    }

    private void SelectApp(string appId)
    {
        AppState.SetApp(appId);

        JS.InvokeVoidAsync("appState.set", "currentApp", appId);
        JS.InvokeVoidAsync("appState.set", "sidebarExpanded", false);
    }
}