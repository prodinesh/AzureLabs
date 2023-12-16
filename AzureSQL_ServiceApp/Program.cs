using AzureSQL_ServiceApp.DAL;
using AzureSQL_ServiceApp.Interface;
using Microsoft.FeatureManagement;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

//var connectionstring = "Endpoint=https://delconfig.azconfig.io;Id=M0cF;Secret=y4DmcxMwxMwfcKFO++nrvij+0yZ2SPjjsej90Wfi/wE=";

//builder.Configuration.AddAzureAppConfiguration(
//      options => options.Connect(connectionstring).UseFeatureFlags()
//);

//builder.Host.ConfigureAppConfiguration(builder 
//    => builder.AddAzureAppConfiguration(options 
//        => options.Connect(connectionstring).UseFeatureFlags())
//);

builder.Services.AddFeatureManagement();
    
builder.Services.AddTransient<IBooksService, BooksService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
