using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.Appointments.Domain.Repositories;
using Testcase.Appointments.Infrastructure.Data;
using Testcase.Appointments.Infrastructure.Data.Interfaces;
using Testcase.Appointments.Infrastructure.Settings;
using Testcase.Appointments.Infrastructure.Settings.Interfaces;


namespace Testcase.Appointments.Infrastructure
{
    public static class DependcyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<AppoinmentsSettings>(configuration.GetSection(nameof(AppoinmentsSettings)));
            services.AddSingleton<IAppoinmentsSettings>(sp => sp.GetRequiredService<IOptions<AppoinmentsSettings>>().Value);
            #endregion



            #region ProjectDependencies
            services.AddTransient<IAppoinmentContext, AppoinmentsContext>();
            services.AddTransient<IAppointmentRepository, AppointmentsRepository>();
            #endregion
            return services;
        }
    }
}
