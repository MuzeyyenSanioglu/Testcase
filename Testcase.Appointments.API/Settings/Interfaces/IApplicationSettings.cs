using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.Appointments.API.Settings.Interfaces
{
    public interface IApplicationSettings
    {
        public string UserServiceUrl { get; set; }

        public string ServiceUsername { get; set; }
        public string ServicePassword { get; set; } 
    }
}
