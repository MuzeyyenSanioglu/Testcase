using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Testcase.Appointments.API.Services;
using Testcase.Appointments.API.Settings;
using Testcase.Appointments.API.Settings.Interfaces;

namespace Testcase.Appointments.API.Helper
{
    public static class AuthConfiguration
    {
        public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection(nameof(ApplicationSettings)));
            services.AddSingleton<IApplicationSettings>(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);
            services.AddScoped<IUserServices, UserService>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetSection("TokenOptions:Issuer").Value,
                    ValidAudience = Configuration.GetSection("TokenOptions:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("TokenOptions:SecurityKey").Value))
                };
            });
        }
    }
}
