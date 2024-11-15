using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sunergizer_API.Configuration;
using Sunergizer_API.Database;
using Sunergizer_API.Services;

namespace Sunergizer_API.Extensions
{
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IComunidadeService, ComunidadeService>();
            service.AddScoped<IConsumoService, ConsumoService>();
            service.AddScoped<IFonteEnergiaService, FonteEnergiaService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddSingleton<EnergiaPredictionService>();

            return service;
        }

        public static IServiceCollection AddDBContexts(this IServiceCollection service, AppConfiguration appConfiguration)
        {
            service.AddDbContext<SunergizerDBContext>(options =>
            {
                options.UseOracle(appConfiguration.ConnectionStrings.OracleSunergizer);
            });

            return service;
        }


        public static IServiceCollection AddSwagger(this IServiceCollection service, AppConfiguration appConfiguration)
        {

            service.AddSwaggerGen(swagger =>
            {
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = appConfiguration.Swagger.Title,
                    Version = "v1",
                    Description = appConfiguration.Swagger.Description,
                }
                );
            });


            return service;
        }
    }
}
