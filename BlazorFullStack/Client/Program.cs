global using BlazorFullStack.Shared;
global using BlazorFullStack.Client.Services.SuperHeroes;
using BlazorFullStack.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ISuperHeroesService, SuperHeroesService>();


await builder.Build().RunAsync();
