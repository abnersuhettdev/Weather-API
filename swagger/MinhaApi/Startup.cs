using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.IO;
using System.Reflection;

/// <summary>
/// Classe responsável pela configuração da aplicação no momento de inicialização.
/// </summary>
public class Startup
{
    /// <summary>
    /// Configuração dos serviços da aplicação.
    /// </summary>
    /// /// <param name="services">Coleção de serviços da aplicação.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast API", Description = "API para demonstração do Swagger", Version = "v1" });

            var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, filename);
            c.IncludeXmlComments(filePath);
            // Adiciona um filtro para personalizar o swagger.json
            c.DocumentFilter<CustomDocumentFilter>();
        });
    }

    public class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Aqui você pode fazer modificações no objeto swaggerDoc conforme necessário
            // Por exemplo, remover ou adicionar informações
            swaggerDoc.Info.Description = " (Modificado por um filtro)";
            swaggerDoc.Info.Title = "Api do Tempo";
            foreach (var (path, pathItem) in swaggerDoc.Paths)
            {
                // Iterar sobre as operações (métodos HTTP) para cada caminho
                foreach (var (method, operation) in pathItem.Operations)
                {
                    // Verificar se a operação possui uma resposta de sucesso (código 200)
                    if (operation.Responses.ContainsKey("200"))
                    {
                        // Modificar a descrição da resposta de sucesso
                        operation.Responses["200"].Description = "Operação bem-sucedida";
                    }
                }
            }

            // Certifique-se de que a chave '/api/WeatherForecast' existe no dicionário
            if (swaggerDoc.Paths.TryGetValue("/api/WeatherForecast", out var itemPath))
            {
                // Certifique-se de que a operação 'get' existe no item de caminho
                if (itemPath.Operations.TryGetValue(OperationType.Get, out var operation))
                {
                    // Agora você pode acessar a propriedade 'summary' dentro do objeto 'operation'
                    operation.Summary = "Tempo";
                }
            }

             // Adicionando uma tag personalizada
            var customTag = new OpenApiTag
            {
                Name = "CustomTag",
                Description = "Tag personalizada"
            };

            if (swaggerDoc.Tags == null)
            {
                swaggerDoc.Tags = new List<OpenApiTag>();
            }

            swaggerDoc.Tags.Add(customTag);

        }
    }

    /// <summary>
    /// Configuração da aplicação.
    /// </summary>
    /// <param name="app">Builder de aplicação.</param>
    /// <param name="env">Ambiente de hospedagem.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configuração do Swagger no ambiente de desenvolvimento
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de teste V1"));
        }

        app.UseSwagger();

        // Configuração do Swagger e Swagger UI 
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo API");
            options.RoutePrefix = "";
        });

        // Configuração de roteamento
        app.UseRouting();

        // Configuração de autorização
        app.UseAuthorization();

        // Configuração de endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Redirecionamento HTTPS
        app.UseHttpsRedirection();


    }
}