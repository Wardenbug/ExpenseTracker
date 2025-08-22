using ExpenseTracker.Api.Expenses;
using ExpenseTracker.Api.Users;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();
    using var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    using var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

    appDbContext.Database.Migrate();
    identityDbContext.Database.Migrate();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapExpensesEndpoints();
app.MapUsersEndpoints();
app.UseStatusCodePages();

app.Run();