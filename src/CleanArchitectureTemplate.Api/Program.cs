using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Infrastructure;
using CleanArchitectureTemplate.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();