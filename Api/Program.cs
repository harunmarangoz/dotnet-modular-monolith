using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

var services = builder.Services;
var configuration = builder.Configuration;
var startup = new Startup();

startup.ConfigureServices(services, configuration);

var app = builder.Build();

startup.Configure(app, app.Environment);

await app.RunAsync();