using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorHRM.Client;
using BlazorHRM.Client.Services;
using Sotsera.Blazor.Toaster.Core.Models;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Web Service Scopes
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IJobCategoryService, JobCategoryService>();
#endregion

#region Public MIT Services

builder.Services.AddToaster(config =>
{
    config.PositionClass = Defaults.Classes.Position.TopRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = false;
});

builder.Services.AddBlazoredModal();

#endregion


await builder.Build().RunAsync();

