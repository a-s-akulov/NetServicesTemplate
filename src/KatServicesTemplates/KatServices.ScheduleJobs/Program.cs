using KatServices.ScheduleJobs;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.Logger.LogInformation("--- ������ ������ KatServices.ScheduleJobs ---");

app.ConfigureApplication();

app.Run();