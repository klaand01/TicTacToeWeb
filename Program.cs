using TicTacToeWeb.Data;
using TicTacToeWeb.Endpoints;
using TicTacToeWeb.Repository;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseInMemoryDatabase("Board");
});
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureGameEndpoint();
app.ConfigurePlayerXEndpoint();
app.ConfigurePlayerOEndpoint();
app.Run();