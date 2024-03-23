using MecanicaBeneteli.WebApp.Configurations;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddEventLog(eventLogSettings =>
{
    eventLogSettings.SourceName = builder.Configuration.GetSection("CodigoPrograma").Get<string>();
});

builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

//ConfigureServices


builder.Services.AddAutoMapper(typeof(MecanicaBeneteli.Configurations.AutoMapper));
builder.Services.AddMvcConfiguration(builder.Environment.EnvironmentName);
builder.Services.ResolveDependencies(builder.Configuration, builder.Environment.EnvironmentName);

var app = builder.Build();

//Configure

app.UseMvcConfiguration(app.Environment);

app.Run();
