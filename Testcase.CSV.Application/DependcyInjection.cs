using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcase.CSV.Application.Services;
using Testcase.CSV.Application.Services.Interfaces;
using Testcase.CSV.Application.Settings;
using Testcase.CSV.Application.Settings.Interfaces;

namespace Testcase.CSV.Application
{
    public static  class DependcyInjection
    {
        public static IServiceCollection AddApplications(this IServiceCollection services,IConfiguration configuration)
        {

            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
            services.AddSingleton<IApplicationSettings>(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);
            services.AddScoped<ICSVServices,CSVService>();
            services.AddScoped<IAppointmentServices, AppointmentService>();
            return services;
        }
    }
}