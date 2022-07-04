using Testcase.Appointments.Domain.Response;

namespace Testcase.Appointments.API.Services
{
    public interface IUserServices
    {
        APIResponse GetUserById(string id);
    }
}
