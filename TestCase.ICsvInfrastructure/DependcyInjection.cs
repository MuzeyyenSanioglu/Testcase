using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcase.CSV.Domain.Repositories;
using TestCase.ICsvInfrastructure.Data;
using TestCase.ICsvInfrastructure.Data.Interfaces;
using TestCase.ICsvInfrastructure.Repositories;
using TestCase.ICsvInfrastructure.Settings;
using TestCase.ICsvInfrastructure.Settings.Interfaces;

namespace TestCase.ICsvInfrastructure
{
    public static class DependcyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<CSVDatabaseSettings>(configuration.GetSection(nameof(CSVDatabaseSettings)));
            services.AddSingleton<ICSVDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CSVDatabaseSettings>>().Value);
            #endregion


            #region ProjectDependencies
            services.AddTransient<ICSVContext, CSVContext>();
            services.AddTransient<ICSVRepository, CSVRepository>();
            #endregion


            return services;
        }
        
        
    }
}
