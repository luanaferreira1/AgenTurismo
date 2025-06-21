using AgenTurismo.Data;
using AgenTurismo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "BasicAuthentication";
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
})
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
    "BasicAuthentication", null);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AgenciaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == false && 
        !context.Request.Path.StartsWithSegments("/Account") &&
        !context.Request.Path.StartsWithSegments("/_framework") &&
        !context.Request.Path.StartsWithSegments("/css") &&
        !context.Request.Path.StartsWithSegments("/js"))
    {
        context.Response.Redirect("/Account/Login");
        return;
    }
    await next();
});

app.MapRazorPages();

app.Run();