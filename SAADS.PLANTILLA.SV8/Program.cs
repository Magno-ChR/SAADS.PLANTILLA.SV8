using Microsoft.AspNetCore.Components.Authorization;
using SAADS.PLANTILLA.SV8.Auth;
using SAADS.PLANTILLA.SV8.Auth.Auth;
using SAADS.PLANTILLA.SV8.Components;
using SAADS.PLANTILLA.SV8.Components.Layout;
using SAADS.PLANTILLA.SV8.Helpers;
using SAADS.PLANTILLA.WA8.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<IMostrarMensajes, MostrarMensajes>();
builder.Services.AddScoped<IMensajeToastr, MensajeToastr>();
builder.Services.AddScoped<IDataTable, DataTable>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<MainLayout>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddConfRN();

builder.Services.AddScoped<ProveedorAutenticacionJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(
    provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());
builder.Services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(
   provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());
builder.Services.AddScoped<RenovadorToken>();
builder.Services.AddScoped<UserJWT>();

builder.Services.AddConfReposirioHTTP(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
