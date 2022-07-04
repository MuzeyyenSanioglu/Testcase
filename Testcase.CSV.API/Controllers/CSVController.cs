using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Testcase.CSV.Application.Services.Interfaces;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly ICSVServices _csvServices;
        private readonly IAppointmentServices _appointmentServices;

        public CSVController(ICSVServices csvServices,IAppointmentServices appointmentServices)
        {
            _csvServices = csvServices;
            _appointmentServices = appointmentServices;


        }
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult CreateCsvData()
        {
            APIResponse result = new APIResponse();
            List<APIResponse> responseDatas = new List<APIResponse>();
            APIResponse<JObject> apiresult =_appointmentServices.GetAppointmentAll();
            if (!apiresult.IsSuccess)
                return NotFound(apiresult.ErrorMessage);
            if (apiresult.Data["data"].Count() <= 0)
                return NotFound("not exists appoinment");

            List<DateTime> dates = apiresult.Data["data"].Select( s =>(DateTime) s.SelectToken("date")).ToList();
            result = _csvServices.CreateCsvData(dates);
            return Ok(result);
        }
    }
}
