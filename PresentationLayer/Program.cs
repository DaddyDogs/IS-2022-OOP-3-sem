using ApplicationLayer.Extensions;
using ApplicationLayer.Services.Implementations;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Extensions;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
    .UseSqlite("Data source=DataBase.db")
    .UseLazyLoadingProxies();
var dbContext = new DatabaseContext(optionsBuilder.Options);

builder.Services.AddScoped<ISupervisorService, SupervisorService>();
builder.Services.AddApplication();
builder.Services.AddDataAccess(x => x.UseLazyLoadingProxies().UseSqlite("Data source=DataBase.db"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();