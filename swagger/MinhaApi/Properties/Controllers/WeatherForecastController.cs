using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Controlador para obter previsões meteorológicas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
     /// <summary>
    /// Obtém uma lista de previsões meteorológicas.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// GET /api/weatherforecast
    /// </remarks>
    /// <returns>Uma lista de objetos WeatherForecast.</returns>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = $" The weather is: {Summaries[rng.Next(Summaries.Length)]}"
        })
        .ToArray();
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}

/// <summary>
/// Representa uma previsão meteorológica.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Data da previsão.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Temperatura em graus Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

     /// <summary>
    /// Resumo da previsão.
    /// </summary>
    public string? Summary { get; set; }
}
