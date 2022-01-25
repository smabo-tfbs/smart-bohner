using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Serilog.Formatting.Json;
using SmartBohner.ControlUnit.AspNet;
using SmartBohner.ControlUnit.Extensions;
using SmartBohner.Web.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Configure kestrel to listen on port 5001 (Raspberry) and 5002 (Desktop, Mobile, ...)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001);
    options.ListenAnyIP(5002);
});

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

app.UseOpenApi();
app.UseSwaggerUi3();
app.UseSerilogRequestLogging();

app.UseWhen(context => context.Request.Host.Port == 5001, config =>
{
    config.Use((context, next) =>
    {
        context.Request.Path = "/raspy" + context.Request.Path;
        return next();
    });

    config.UseStaticFiles();
    config.UseBlazorFrameworkFiles("/raspy");
    config.UseStaticFiles("/raspy");
    config.UseRouting();

    // TODO: Change cors to given requirements (everything allowed at the moment)
    config.UseCors(c =>
    {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.SetIsOriginAllowed(origin => true);
        c.AllowCredentials();
    });
    
    config.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<WarningHub>("/warnings");
        endpoints.MapFallbackToFile("/raspy/{*path:nonfile}",
            "raspy/index.html");
    });
});

app.UseWhen(context => context.Request.Host.Port == 5002, config =>
{
    config.Use((context, next) =>
    {
        context.Request.Path = "/mobile" + context.Request.Path;
        return next();
    });

    config.UseStaticFiles();
    config.UseBlazorFrameworkFiles("/mobile");
    config.UseStaticFiles("/mobile");
    config.UseRouting();

    // TODO: Change cors to given requirements (everything allowed at the moment)
    config.UseCors(c =>
    {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.SetIsOriginAllowed(origin => true);
        c.AllowCredentials();
    });

    config.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<WarningHub>("/warnings");
        endpoints.MapFallbackToFile("/mobile/{*path:nonfile}",
            "mobile/index.html");
    });
});

app.InitControlUnit();

// Initialize singleton service to subscribe all warnings
app.Services.GetService<IWarningHubNotifier>();

app.Run();
