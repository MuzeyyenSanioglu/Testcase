using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUsers(string id);
        Task<Users> GetUserByUserName(string name);
        Task<Users> GetUserByuserNameandPassword(string username, string password);
        Task Create(Users user);
        Task<bool> Update(Users user);
        Task<bool> Delete(string id);
    }
    
}