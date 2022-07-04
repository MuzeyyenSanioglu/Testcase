using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Application.Settings.Interfaces;

namespace Testcase.CSV.Application.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string AppoinmentServiceUrl { get; set; } = null!;
        public string AuthUrl { get; set; } = null!;
        public string ServiceUsername { get; set; } = null!;
        public string ServicePassword { get; set; } = null!;
    }
}
