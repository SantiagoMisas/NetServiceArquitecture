
using API.ApplicationDb;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using API.DTOS;
using API.Models;
using API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddScoped<IGenericRepository<Person, string>, RepositoryImpl<Person, string>>();
builder.Services.AddScoped<IEdit<PersonDto>, PersonService>();
builder.Services.AddScoped<IRead<PersonDto>, PersonService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
