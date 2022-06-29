using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.Appointments.Domain.Response;

namespace Testcase.Appointments.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<APIResponse> Create(Appointment appointments);
        Task<APIResponse> Update(Appointment appointments);
        Task<APIResponse> Delete(string id);
        Task<APIResponse<IEnumerable<Appointment>>> GetAll();
        Task<APIResponse<IEnumerable<Appointment>>> GetAppointmentsWithuserId(string userId);
        Task<APIResponse> CheckUserAppointment(string userId, DateTime apointmentDate, string timeZone);

    }
}
