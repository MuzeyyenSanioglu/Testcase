using MongoDB.Driver;
using Testcase.Appointments.Infrastructure.Data.Interfaces;
using Testcase.Appointments.Infrastructure.Settings.Interfaces;

namespace Testcase.Appointments.Infrastructure.Data
{
    public class AppoinmentsContext : IAppoinmentContext
    {
        public AppoinmentsContext(IAppoinmentsSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Appointments = database.GetCollection<Domain. Appointment>(settings.CollectionName);
           // UserContextSeed.SeedData(Users);
        }
        public IMongoCollection<Domain.Appointment> Appointments { get; }
    }
}
