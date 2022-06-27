using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.Infrastructure.Settings.Interfaces;

namespace Testcase.Infrastructure.Settings
{
    public class UserDataBaseSettings : IUserDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
