using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5083") });

// Register ProductCache as a singleton to persist across component instances
builder.Services.AddSingleton<ProductCache>();

await builder.Build().RunAsync();