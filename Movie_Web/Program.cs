using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Movie_Web;
using Movie_Web.Mapping;
using Movie_Web.Models;
using Movie_Web.Models.ApiResponse;
using Movie_Web.Services;

var builder = WebApplication.CreateBuilder(args);
var mappingConfig = new MapperConfiguration(mc =>
{
 mc.AddProfile(new AutoMapperProfiles());
});
IMapper mapper= mappingConfig.CreateMapper();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddAuthenticationCore();

builder.Services.AddTransient<IAPIClientService<Movie>,APIClientService<Movie>>();
builder.Services.AddTransient<IAPIClientService<User>, APIClientService<User>>();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
