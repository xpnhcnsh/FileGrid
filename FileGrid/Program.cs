using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using FileGrid.Components;
using Minio;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<FileGrid.Entities.FileGridContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FileGrid")));

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRazorPages();

// Add HttpClient service
// builder.Services.AddHttpClient("FileGridHTTPClient",
//     client => client.BaseAddress = new Uri(builder.Configuration["HTTPClientBaseUri"] ?? "http://localhost:5039/"))
//     .ConfigurePrimaryHttpMessageHandler(() =>
//     {
//         var handler = new HttpClientHandler();
//         handler.UseCookies = true;
//         handler.CookieContainer = new System.Net.CookieContainer();
//         return handler;
//     });

// Add services to Auth
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.LoginPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;
                });
builder.Services.AddAntiforgery();

// builder.Services.Configure<CookiePolicyOptions>(options =>
//             {
//                 options.ConsentCookie.IsEssential = true;
//                 // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//                 options.CheckConsentNeeded = context => false;
//             });

// Add Minio client as singleton service
builder.Services.AddSingleton(new MinioClient()
    .WithEndpoint("localhost:9000")
    .WithCredentials("minioadmin", "minioadmin")
    .Build());
// 添加在其他服务注册之后
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();


app.Run();
