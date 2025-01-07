
using API.ApplicationDb;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using API.DTOS;
using API.Models;
using API.Interfaces;
using API.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

IoCConfiguration.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
