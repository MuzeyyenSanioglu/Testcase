using Newtonsoft.Json.Linq;
using Testcase.Appointments.Domain.Response;

namespace Testcase.Appointments.API.Services
{
    public interface IUserServices
    {
        APIResponse<JObject> GetUserById(string id);
    }
}
