using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

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
    });
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