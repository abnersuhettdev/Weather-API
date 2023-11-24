using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MinhaApi
{
     /// <summary>
    /// Classe principal que contém o método de inicialização da aplicação.
    /// </summary>
    public class Program
    {
         /// <summary>
        /// Ponto de entrada da aplicação.
        /// </summary>
        /// /// <param name="args">Argumentos de linha de comando.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

     /// <summary>
        /// Configura e constrói o host da aplicação.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}