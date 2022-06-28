using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using Testcase.Infrastructure.Concrete;
using Testcase.Infrastructure.Concrete.Interfaces;
using Testcase.Infrastructure.Data.Interfaces;
using Testcase.Infrastructure.Repositories;
using Testcase.Infrastructure.Settings;
using Testcase.Infrastructure.Settings.Interfaces;
using Testcase.User.Domain.Repositories;

namespace Testcase.Infrastructure
{
    public static  class DependcyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<UserDataBaseSettings>(configuration.GetSection(nameof(UserDataBaseSettings)));
            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDataBaseSettings>>().Value);
            #endregion

            services.AddSingleton<IPasswordHashing, BcryptPasswordHashing>();
            services.AddScoped<ITokenHandler, JWTHandler>();
            #region ProjectDependencies
            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<IUserRepository, UserRepository>(); 
            #endregion
            return services;
        }
    }
}
