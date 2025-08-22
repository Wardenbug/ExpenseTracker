using ExpenseTracker.Api.Expenses;
using ExpenseTracker.Api.Middlewares;
using ExpenseTracker.Api.Users;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracing =>
        tracing.AddHttpClientInstrumentation()
               .AddAspNetCoreInstrumentation())
    .WithMetrics(metrics =>
        metrics.AddHttpClientInstrumentation()
               .AddAspNetCoreInstrumentation()
               .AddRuntimeInstrumentation())
    .UseOtlpExporter();

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.IncludeFormattedMessage = true;
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();
    using var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    using var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

    appDbContext.Database.Migrate();
    identityDbContext.Database.Migrate();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapExpensesEndpoints();
app.MapUsersEndpoints();

app.Run();