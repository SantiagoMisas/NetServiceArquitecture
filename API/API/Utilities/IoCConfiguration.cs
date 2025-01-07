﻿using API.DTOS;
using API.Interfaces;
using API.Models;
using API.Repositories;
using API.Services;

namespace API.Utilities
{
    public static class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Person, string>, RepositoryImpl<Person, string>>();
            services.AddScoped<IEdit<PersonDto>, PersonService>();
            services.AddScoped<IRead<PersonDto>, PersonService>();

        }
    }
}