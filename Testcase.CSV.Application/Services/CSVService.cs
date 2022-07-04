using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Application.Helper;
using Testcase.CSV.Application.Services.Interfaces;
using Testcase.CSV.Application.Settings.Interfaces;
using Testcase.CSV.Domain;
using Testcase.CSV.Domain.Repositories;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Application.Services
{
    public class CSVService : ICSVServices
    {
        private readonly ICSVRepository _csvRepository;
        private readonly string filePath;
        public CSVService(ICSVRepository csvRepository , IApplicationSettings settings)
        {
            _csvRepository = csvRepository;
            filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "CSVFiles"); ;
        }

        public APIResponse<List<string>> GetCSVFileName()
        {
            APIResponse<List<string>> response = new APIResponse<List<string>>();
            List<string> data = Directory.GetFiles(filePath, "*.*").ToList();
            response.SetData(data);
            return response;
        }

        public APIResponse CreateCSVFile(string fileName)
        {
            APIResponse response = new APIResponse();
            try
            {
                Utility utility = new Utility();
                string convertedName = fileName.Split("\\").LastOrDefault();
                var nameData = utility.ConvertToNameFromCSVFile(convertedName);
                Domain.CSV csv = new Domain.CSV();
                csv.id = Guid.NewGuid().ToString();
                User csvUser = new User();
                csvUser.UserId = nameData["UserId"].ToString();

                Appointment appointment = new Appointment();
                appointment.appoinmentId = nameData["AppoinmentId"].ToString();
                csvUser.appoinments = appointment;

                Test test = new Test();
                test.TestId = nameData["TestId"].ToString();

                DataTable dataTable = utility.ConvertCSVtoDataTable(fileName);
                test.Datas = JsonConvert.SerializeObject(dataTable, Formatting.Indented);

                appointment.tests = test;
                csvUser.appoinments = appointment;
                csv.user = csvUser;

               APIResponse result = _csvRepository.Create(csv).Result;
               response.ObjectId = csv.id;
               response.SetSuccess();

                if (!result.IsSuccess)
                    response.SetFailure(result.ErrorMessage);
                
            }
            catch (Exception ex)
            {
                response.SetFailure(ex);
            }
            return response;
        }

        public APIResponse CreateCsvData(List<DateTime> dates)
        {
            APIResponse<List<APIResponse>> result = new APIResponse<List<APIResponse>>();
            List<APIResponse> responseDatas = new List<APIResponse>();
            List<string> files = GetCSVFileName().Data;
            foreach (string item in files)
            {
                var dateCloumn = item.Split("_");
                var fileDate = dateCloumn[3].Split("(")[0].ToString();
                DateTime dateResult = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(fileDate)); 
                if (dates.Contains(dateResult.Date))
                {
                    
                    APIResponse response = CreateCSVFile(item);
                    responseDatas.Add(response);
                }
                    
            }
            result.SetData(responseDatas);
            return result;
        }
    }
}
