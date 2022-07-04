using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.Infrastructure.Data.Interfaces;
using Testcase.User.Domain;
using Testcase.User.Domain.Repositories;
using Testcase.User.Domain.Responses;

namespace Testcase.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> CheckUserByExist(string username)
        {
            APIResponse result = new APIResponse();
            try
            {
                var user = _context.Users.Find(p => p.UserName == username).FirstOrDefaultAsync().Result;
                if (user != null)
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

        public async Task<APIResponse> Create(Users user)
        {
            APIResponse result = new APIResponse();
            try
            {
                await _context.Users.InsertOneAsync(user);
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
                var filter = Builders<Users>.Filter.Eq(m => m.UserId, id);
                DeleteResult deleteResult = await _context.Users.DeleteOneAsync(filter);
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

        public async Task<APIResponse<Users>> GetUserByUserName(string name)
        {
            APIResponse<Users> result = new APIResponse<Users>();
            try
            {
                Users user = await _context.Users.Find(p => p.UserName == name).FirstOrDefaultAsync();
                result.SetData(user);
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }
                

        public async Task<APIResponse<Users>> GetUserByuserNameandPassword(string username, string password)
        {
            APIResponse<Users> result = new APIResponse<Users>();
            try
            {
                Users user = await _context.Users.Find(p => p.UserName == username && p.Password == password)
                        .FirstOrDefaultAsync();
                result.SetData(user);
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse<IEnumerable<Users>>> GetUsers()
        {
            APIResponse<IEnumerable<Users>> result = new APIResponse<IEnumerable<Users>>();

            try
            {
                IEnumerable<Users> users =  await _context.Users.Find(P => true).ToListAsync();
                result.SetData(users);
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse<Users>> GetUser(string id)
        {
            APIResponse < Users > result = new APIResponse<Users>();
            try
            {
                Users user = await _context.Users.Find(p => p.UserId == id).FirstOrDefaultAsync();
                if (user != null)
                    result.SetData(user);
                else result.SetFailure("User Not Found");
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse> Update(Users user)
        {
            APIResponse result = new APIResponse();
            try
            {
                var updateResult = await _context.Users.ReplaceOneAsync(filter: g => g.UserId == user.UserId, replacement: user);
                 
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
