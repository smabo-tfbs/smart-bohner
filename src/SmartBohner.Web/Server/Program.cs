using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Serilog.Formatting.Json;
using SmartBohner.ControlUnit.AspNet;
using SmartBohner.ControlUnit.Extensions;
using SmartBohner.Web.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterControlUnit();
builder.Services.AddSingleton<IWarningHubNotifier, WarningHubNotifier>();
builder.Services.AddSwaggerDocument(c =>
{
    c.Title = "Smart-Bohner API";
    c.Version = "v1";
    c.Description = "Communicate with smart coffee machine.";
});

builder.Services.AddSignalR()
    .AddMessagePackProtocol();

LoggerConfiguration configuration = new LoggerConfiguration();
configuration
    .Enrich.WithProperty("Application", "smabo-webapi")
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(
            new JsonFormatter(),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "smabo", "logs", "log.json"),
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 1024 * 1024 * 1024,
            shared: true);

builder.WebHost.UseSerilog(configuration.CreateLogger());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression(c =>
{
    // Configure default mime-types to compress octet-streams (SignalR)
    c.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(c =>
{
    // TODO: Configure cors for requirements (allows everything at the moment)
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.SetIsOriginAllowed(origin => true);
    c.AllowCredentials();
});

app.UseOpenApi();
app.UseSwaggerUi3();
app.UseSerilogRequestLogging();

app.MapRazorPages();
app.MapControllers();

// TODO: Map warning hub (waiting for deployment branch)
app.MapHub<WarningHub>("/warnings");

app.MapFallbackToFile("index.html");

app.InitControlUnit();

// Initialize singleton service to subscribe all warnings
app.Services.GetService<IWarningHubNotifier>();

app.Run();
