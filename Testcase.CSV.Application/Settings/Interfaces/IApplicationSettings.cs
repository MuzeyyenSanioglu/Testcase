using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.CSV.Application.Settings.Interfaces
{
    public interface IApplicationSettings
    {
        public string FilePath { get; set; }
        public string AppoinmentServiceUrl { get; set; }
        public string AuthUrl { get; set; }
        public string ServiceUsername { get; set; }
        public string ServicePassword { get; set; }

    }
}
