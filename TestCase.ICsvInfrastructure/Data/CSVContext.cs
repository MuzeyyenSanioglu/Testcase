using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain;
using TestCase.ICsvInfrastructure.Data.Interfaces;
using TestCase.ICsvInfrastructure.Settings.Interfaces;

namespace TestCase.ICsvInfrastructure.Data
{
    public class CSVContext : ICSVContext
    {
        public CSVContext(ICSVDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            CVSs = database.GetCollection<CSV>(settings.CollectionName);
            
        }
        public IMongoCollection<CSV> CVSs { get; }
    }
}
