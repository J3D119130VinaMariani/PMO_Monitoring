using Microsoft.EntityFrameworkCore;
using PMO_Monitoring.Controllers;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PMO_MonitoringContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<LoginPageController, LoginPageController>();
builder.Services.AddScoped<TrxDaftarAktifitasRepo>();
builder.Services.AddScoped<TrxProjectRepo>();
builder.Services.AddScoped<MstHomeRepo>();
builder.Services.AddScoped<HomeController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    //app.UseExceptionHandler("/Home/Error");
//    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    //app.UseHsts();
//}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginPage}/{action=Index}/{id?}");

app.Run();
