using ExpenseTracker.Api.Expenses;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapExpensesEndpoints();

app.Run();