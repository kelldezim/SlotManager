using SlotManager.Infrastructure;
using SlotManager.Core;
using SlotManager.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
