using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.User.Domain;

namespace Testcase.Infrastructure.Data
{
    public class UserContextSeed
    {

        public static void SeedData(IMongoCollection<Users> userCollection)
        {
            bool existProduct = userCollection.Find(p => true).Any();
            if (!existProduct)
            {
                userCollection.InsertManyAsync(GetConfigureUsers());
            }
        }

        public static IEnumerable<Users> GetConfigureUsers()
        {
            return new List<Users>() {
                new Users()
                {
                   UserName ="testUser1",
                   Password ="123456789"
                   
                },
                new Users()
                {
                   UserName ="testUser2",
                   Password ="123456789"

                },
                new Users()
                {
                   UserName ="testUser3",
                   Password ="123456789"

                }
            };
        }
    }
}
