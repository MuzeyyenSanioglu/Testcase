using MongoDB.Driver;

namespace Testcase.Appointments.Infrastructure.Data.Interfaces
{
    public interface IAppoinmentContext
    {
        IMongoCollection<Domain.Appointment> Appointments { get; }
    }
}
