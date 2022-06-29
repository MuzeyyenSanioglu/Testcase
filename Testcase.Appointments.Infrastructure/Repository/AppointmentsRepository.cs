using MongoDB.Driver;
using Testcase.Appointments.Domain;
using Testcase.Appointments.Domain.Repositories;
using Testcase.Appointments.Domain.Response;
using Testcase.Appointments.Infrastructure.Data.Interfaces;

namespace Testcase.Appointments.Infrastructure
{
    public class AppointmentsRepository : IAppointmentRepository
    {
        IAppoinmentContext _context;
        public AppointmentsRepository(IAppoinmentContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> CheckUserAppointment(string userId, DateTime apointmentDate, string timeZone)
        {
           APIResponse result = new APIResponse();
            try
            {
                Appointment appointment = await _context.Appointments
                    .Find(p => p.UserId == userId && p.Date == apointmentDate && p.TimeSlot == timeZone)
                    .FirstOrDefaultAsync();
                if (appointment != null)
                    result.AlreadyExist = true;
                else
                    result.AlreadyExist = false;
                result.SetSuccess();

            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse> Create(Domain.Appointment appointments)
        {
            APIResponse result = new APIResponse();
            try
            {
                await _context.Appointments.InsertOneAsync(appointments);
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse> Delete(string id)
        {
            APIResponse result = new APIResponse();
            try
            {
                var filter = Builders<Domain.Appointment>.Filter.Eq(m => m.AppointmetsId, id);
                DeleteResult deleteResult = await _context.Appointments.DeleteOneAsync(filter);
                if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
                    result.SetSuccess();
                else
                    result.SetFailure();
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse<IEnumerable<Appointment>>> GetAll()
        {
            APIResponse<IEnumerable<Appointment>> result = new APIResponse<IEnumerable<Appointment>>();
            try
            {
                IEnumerable<Appointment> appointmnets = await _context.Appointments.Find(s => true).ToListAsync();
                result.SetData(appointmnets);
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse<IEnumerable<Appointment>>> GetAppointmentsWithuserId(string userId)
        {
            APIResponse<IEnumerable<Appointment>> result = new APIResponse<IEnumerable<Appointment>>();
            try
            {
                IEnumerable<Appointment> appointments = await _context.Appointments.Find(s => s.UserId == userId).ToListAsync();
                result.SetData(appointments);
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse> Update(Appointment appointments)
        {
            APIResponse result = new APIResponse();
            try
            {
                var updateResult = await _context.Appointments.ReplaceOneAsync(filter: g => g.AppointmetsId == appointments.AppointmetsId, replacement: appointments);
                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    result.SetSuccess();
                else
                    result.SetFailure();
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }
    }
}