using BlackEnd.Application.Commands;
using BlackEnd.Application.Validators;
using BlackEnd.Domain.Interfaces;
using BlackEnd.Infrastructure.Context;
using BlackEnd.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlackEnd.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<BlackEndContext>(options =>
                 options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<BlackEndContext>();

            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IValidator<CreateClienteCommand>, CreateClienteCommandValidator>();

            services.AddScoped<IValidator<CreateClienteCommand>, CreateClienteCommandValidator>();

            services.AddScoped<IValidator<UpdateClienteCommand>, UpdateClienteCommandValidator>();

            return services;
        }
    }
}
