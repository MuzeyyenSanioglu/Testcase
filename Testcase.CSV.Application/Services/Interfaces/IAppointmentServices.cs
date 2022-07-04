using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Application.Services.Interfaces
{
    public interface IAppointmentServices
    {
        APIResponse<string> Auth();

        APIResponse<JObject> GetAppointmentAll();
    }
}
