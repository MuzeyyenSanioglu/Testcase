using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Application.Helper;
using Testcase.CSV.Application.Models;
using Testcase.CSV.Application.Services.Interfaces;
using Testcase.CSV.Application.Settings.Interfaces;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Application.Services
{
    public class AppointmentService : IAppointmentServices
    {
        private IApplicationSettings  _settings ;
        
        public AppointmentService(IApplicationSettings settings)
        {
            _settings = settings ;
        }

        public APIResponse<string> Auth()
        {
            APIResponse<string> result = new APIResponse<string>();
            dynamic body = new
            {
                username = _settings.ServiceUsername,
                password = _settings.ServicePassword,
            };
            string parameter = JsonConvert.SerializeObject(body);
            APIResponse<AuthResponse> authResult = HttpRequestBuilder.GetInstance(_settings.AuthUrl).Post()
                .AddJsonBody(parameter)
                .SendAsync<AuthResponse>().Result;
            if (!authResult.IsSuccess)
            {
                result.SetFailure($"{authResult.ErrorMessage} Could not create token.  Token url : {_settings.AuthUrl} check connection url ");
                return result;
            }
            else if (authResult.Data == null || authResult.Data.Token == null || authResult.Data.Token.Length < 1)
            {
                result.SetFailure("Empty token");
            }
            else
            {
                result.SetData(authResult.Data.Token);
            }
            return result;
        }

        public APIResponse<JObject> GetAppointmentAll()
        {
            APIResponse<JObject> results= new APIResponse<JObject> ();
            APIResponse<string> auth = Auth();
            if (!auth.IsSuccess)
            {
                results.SetFailure("System error : Token  "+auth.ErrorMessage);
                return results;
            }
            APIResponse<JObject> result =   HttpRequestBuilder.GetInstance(_settings.AppoinmentServiceUrl).Get().AddAuthorization(auth.Data).SendAsync<JObject>().Result;
            if (!result.IsSuccess)
            {
                results.SetFailure(result.ErrorMessage + "System Error :  Colud not find exists data.");
                return results;
            }
            results.SetData(result.Data);

            return results;
        }
    }
}
