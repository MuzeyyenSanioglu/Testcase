using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.User.Domain;

namespace Testcase.Infrastructure.Data.Interfaces
{
    public interface IUserContext
    {
        IMongoCollection<Users> Users { get; }
    }
}
