using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MiniCoop.Web.Services;
using MiniCoop.Web.State;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// Razor Pages + Blazor Server
// =======================================
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// =======================================
// Authorization (Blazor UI only)
// =======================================
builder.Services.AddAuthorizationCore();

// =======================================
// Protected Browser Storage
// =======================================
builder.Services.AddScoped<ProtectedLocalStorage>();

// =======================================
// Token Store Memory Storage
// =======================================
builder.Services.AddSingleton<TokenStore>();

// =======================================
// AuthenticationStateProvider
// =======================================
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

// =======================================
// Http Message Handler (แนบ JWT)
// =======================================
builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

// =======================================
// AuthService (Login เท่านั้น / ไม่แนบ JWT)
// =======================================
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});

// =======================================
// API Client (แนบ JWT อัตโนมัติ)
// =======================================
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddScoped<ApiService>();

// =======================================
// Radzen Services
// =======================================
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();

// =======================================
// State
// =======================================
builder.Services.AddScoped<AppState>();

var app = builder.Build();

// =======================================
// Middleware
// =======================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// =======================================
// Blazor Endpoints
// =======================================
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();