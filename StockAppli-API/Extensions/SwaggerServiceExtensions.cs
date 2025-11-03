using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace StockAppli_API.Extensions
{
    public static class SwaggerServiceExtensions
    { /// <summary>
      /// Add config swagger
      /// </summary>
      /// <param name="services"></param>
      /// <returns></returns>
      /// 

        // Assurez-vous d'avoir installé le package NuGet Swashbuckle.AspNetCore
        // dotnet add package Swashbuckle.AspNetCore
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StockAppliAPI",
                    Description = "This is a microservice StockAppliAPI",
                    Version = "v1",
                });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });

                //////Add Operation Specific Authorization///////
                c.OperationFilter<AuthOperationFilter>();
            });

            return services;
        }
        /// <summary>
        /// Generer swagger un fichier JSON endpoint
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockAppli API");
                c.DocumentTitle = "StockAppli API";
                c.RoutePrefix = "api-docs";
                c.DefaultModelRendering(ModelRendering.Example);
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
