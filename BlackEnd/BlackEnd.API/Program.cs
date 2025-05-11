using BlackEnd.Application.Validators;
using BlackEnd.Infrastructure.Context;
using BlackEnd.Infrastructure.Extensions;
using BlackEnd.Infrastructure.IoC;
using BlackEnd.Infrastructure.Mappings;
using BlackEnd.Infrastructure.Seed;
using FluentValidation.AspNetCore;
using MediatR;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Permitir qualquer origem
              .AllowAnyMethod() // Permitir qualquer método (GET, POST, PUT, DELETE, etc)
              .AllowAnyHeader(); // Permitir qualquer cabeçalho
    });
});


builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateClienteCommandValidator>();
        config.DisableDataAnnotationsValidation = true;
        config.AutomaticValidationEnabled = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Verifica se o comando contém --seed e qual seed será executado
var seedCommandIndex = Array.IndexOf(args, "--seed");
if (seedCommandIndex >= 0 && args.Length > seedCommandIndex + 1)
{
    var seedName = args[seedCommandIndex + 1];
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BlackEndContext>();
        ExecuteSeed(seedName, context);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlackEnd API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


void ExecuteSeed(string seedName, BlackEndContext context)
{
    switch (seedName)
    {
        case "SeedClientes":
            DbSeeder.SeedClientes(context);
            Console.WriteLine("SeedClientes executado com sucesso.");
            break;

        // Aqui você pode adicionar outros seeds
        case "SeedProdutos":
            // Exemplo: DbSeeder.SeedProdutos(context);
            Console.WriteLine("SeedProdutos executado com sucesso.");
            break;

        default:
            Console.WriteLine($" Seed {seedName} não encontrado.");
            break;
    }
}