using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Domain.Repositories
{
    public  interface ICSVRepository
    {
        Task<APIResponse> Create(CSV csv);
        Task<APIResponse> Update(CSV csv);
        Task<APIResponse> Delete(string id);
        Task<APIResponse<IEnumerable<CSV>>> GetAll();
    }
}
