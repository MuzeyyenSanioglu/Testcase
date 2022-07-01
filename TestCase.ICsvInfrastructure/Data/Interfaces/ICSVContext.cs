using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain;

namespace TestCase.ICsvInfrastructure.Data.Interfaces
{
    public interface ICSVContext
    {
        IMongoCollection<CSV> CVSs { get; }
    }
}
