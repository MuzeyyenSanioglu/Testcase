using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.User.Domain.Responses;

namespace Testcase.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<APIResponse<IEnumerable<Users>>> GetUsers();
        Task<APIResponse<Users>> GetUser(string id);
        Task<APIResponse<Users>> GetUserByUserName(string name);
        Task<APIResponse> CheckUserByExist(string username);
        Task<APIResponse<Users>> GetUserByuserNameandPassword(string username, string password);
        Task<APIResponse> Create(Users user);
        Task<APIResponse> Update(Users user);
        Task<APIResponse> Delete(string id);
    }
    
}