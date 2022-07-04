using Testcase.Appointments.API.Settings.Interfaces;

namespace Testcase.Appointments.API.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string UserServiceUrl { get; set; } = null!;
        public string ServiceUsername { get; set; } = null!;
        public string ServicePassword { get; set; } = null!;

    }
}
