
using Microsoft.Extensions.DependencyInjection;
using SAADS.PLANTILLA.SV8.Repositorios;



namespace SAADS.PLANTILLA.WA8.Configuration
{
    public static class confHttpClient
    {
        public static IHttpClientBuilder AddConfReposirioHTTP(this IServiceCollection services, ConfigurationManager configuration)
        {


            return services.AddHttpClient<IRepositorio, Repositorio>((serviceProvider, options) =>
            {
                options.BaseAddress = new Uri(configuration["endpoints:api"]!);
            });
        }
    }
}