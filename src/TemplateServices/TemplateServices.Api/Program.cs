using $safeprojectname$;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.Logger.LogInformation("--- ������ ������ $safeprojectname$ ---");

app.ConfigureApplication();

app.Run();
