using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorStandAloneAADApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
        { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("api://8e2beae8-ce0a-4d93-9101-a093490ffca1/User.Read");
    options.ProviderOptions.LoginMode = "redirect";


});

await builder.Build().RunAsync();
