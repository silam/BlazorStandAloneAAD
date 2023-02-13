using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorStandAloneAADApp;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
        { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//builder.Services.AddHttpClient("BlazorWasmWithAADAuth.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
  //     .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
//builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWasmWithAADAuth.ServerAPI"));
//if you are trying to get the token for calling another resource you will have to create another ht



builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("api://8e2beae8-ce0a-4d93-9101-a093490ffca1/User.Read");
    options.ProviderOptions.LoginMode = "redirect";

    //options.ProviderOptions.DefaultAccessTokenScopes.Add("{SCOPE URI}");

});

await builder.Build().RunAsync();
