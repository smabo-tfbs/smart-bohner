using Serilog;
using Serilog.Formatting.Json;
using SmartBohner.ControlUnit.AspNet;
using SmartBohner.ControlUnit.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterControlUnit();

builder.Services.AddSwaggerDocument(c =>
{
    c.Title = "Smart-Bohner API";
    c.Version = "v1";
    c.Description = "Communicate with smart coffee machine.";
});

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

var app = builder.Build();

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

app.UseOpenApi();
app.UseSwaggerUi3();
app.UseSerilogRequestLogging();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.InitControlUnit();
app.Run();
