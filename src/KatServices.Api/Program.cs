using KatServices.Api;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.Logger.LogInformation("--- ������ ������ KatServices.Api ---");

app.ConfigureApplication();

app.Run();
