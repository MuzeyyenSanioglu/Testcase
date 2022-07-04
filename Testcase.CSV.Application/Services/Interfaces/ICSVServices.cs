using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Application.Services.Interfaces
{
    public interface ICSVServices
    {
        APIResponse CreateCSVFile(string fileName);
        APIResponse<List<string>> GetCSVFileName();
        APIResponse CreateCsvData(List<DateTime> dates);
    }
}
