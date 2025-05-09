using BlackEnd.Infrastructure.Extensions;
using BlackEnd.Infrastructure.IoC;
using BlackEnd.Infrastructure.Mappings;
using MediatR;
using FluentValidation.AspNetCore;
using BlackEnd.Application.Validators;

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
