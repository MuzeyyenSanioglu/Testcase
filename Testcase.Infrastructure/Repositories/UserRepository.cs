using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.Infrastructure.Data.Interfaces;
using Testcase.User.Domain;
using Testcase.User.Domain.Repositories;

namespace Testcase.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task Create(Users user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Users>.Filter.Eq(m => m.UserId, id);
            DeleteResult deleteResult = await _context.Users.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Users> GetUserByUserName(string name)
        {
            return await _context.Users.Find(p => p.UserName == name ).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserByuserNameandPassword(string username, string password)
        {
            return await _context.Users.Find(p => p.UserName == username && p.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context.Users.Find(P => true).ToListAsync();
        }

        public async Task<Users> GetUsers(string id)
        {
            return await _context.Users.Find(p => p.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Users user)
        {
            var updateResult = await _context.Users.ReplaceOneAsync(filter: g => g.UserId == user.UserId, replacement: user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
