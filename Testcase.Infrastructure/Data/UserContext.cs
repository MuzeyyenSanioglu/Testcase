using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Testcase.Infrastructure.Data;
using Testcase.Infrastructure.Data.Interfaces;
using Testcase.Infrastructure.Settings.Interfaces;
using Testcase.User.Domain;

namespace Testcase.Infrastructure
{
    public class UserContext : IUserContext
    {
        public UserContext(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Users = database.GetCollection<Users>(settings.CollectionName);
            UserContextSeed.SeedData(Users);
        }
        public IMongoCollection<Users> Users { get; }
    }
}