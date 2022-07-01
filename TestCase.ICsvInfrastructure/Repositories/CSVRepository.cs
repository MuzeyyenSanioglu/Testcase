using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain;
using Testcase.CSV.Domain.Repositories;
using Testcase.CSV.Domain.Responses;
using TestCase.ICsvInfrastructure.Data.Interfaces;

namespace TestCase.ICsvInfrastructure.Repositories
{
    public class CSVRepository : ICSVRepository
    {
        private readonly ICSVContext _context;

        public CSVRepository(ICSVContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Create(CSV csv)
        {
            APIResponse result = new APIResponse();
            try
            {
               await  _context.CVSs.InsertOneAsync(csv);
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
                var filter = Builders<CSV>.Filter.Eq(m => m.id, id);
                DeleteResult deleteResult = await _context.CVSs.DeleteOneAsync(filter);
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

        public async Task<APIResponse<IEnumerable<CSV>>> GetAll()
        {
            APIResponse<IEnumerable<CSV>> result = new APIResponse<IEnumerable<CSV>>();
            try
            {
                IEnumerable<CSV> cvss = await _context.CVSs.Find(s => true).ToListAsync();
                result.SetData(cvss);

            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }

        public async Task<APIResponse> Update(CSV csv)
        {
            APIResponse result = new APIResponse();
            try
            {
                var updateResult = await _context.CVSs.ReplaceOneAsync(filter: g => g.id == csv.id, replacement: csv);

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
