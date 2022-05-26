using Microsoft.AspNetCore.Builder;
using SolicitudesAPI;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

// Configure the HTTP request pipeline.


app.Run();

