using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartBohner.Web.Client;
using SmartBohner.Web.Client.Infrastructure;
using SmartBohner.Web.Client.Infrastructure.Mock;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var mockWarningHandler = builder.Configuration.GetValue<bool>("MockWarningHandler");
if (mockWarningHandler)
    builder.Services.AddSingleton<IWarningHubConnectionHandler, MockWarningHubConnectionHandler>();
else
    builder.Services.AddSingleton<IWarningHubConnectionHandler, WarningHubConnectionHandler>();

await builder.Build().RunAsync();
