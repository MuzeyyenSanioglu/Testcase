using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Testcase.Appointments.API.Helper;
using Testcase.Appointments.API.Models;
using Testcase.Appointments.API.Settings.Interfaces;
using Testcase.Appointments.Domain.Response;

namespace Testcase.Appointments.API.Services
{
    public class UserService : IUserServices
    {
        private IApplicationSettings _settings;

        public UserService(IApplicationSettings settings)
        {
            _settings = settings;
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
            string baseUrl = $"{_settings.UserServiceUrl}login";
            APIResponse<AuthResponse> authResult = HttpRequestBuilder.GetInstance(baseUrl).Post()
                .AddJsonBody(parameter)
                .SendAsync<AuthResponse>().Result;
            if (!authResult.IsSuccess)
            {
                result.SetFailure($"{authResult.ErrorMessage} Could not create token.  Token url : {baseUrl} check connection url ");
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
        public APIResponse GetUserById(string id)
        {
            APIResponse results = new APIResponse();
            string baseUrl = _settings.UserServiceUrl.ToString() + id;
            APIResponse<string> auth = Auth();
            if (!auth.IsSuccess)
            {
                results.SetFailure("System error : Token  " + auth.ErrorMessage);
                return results;
            }
            APIResponse result = HttpRequestBuilder.GetInstance(baseUrl).AddAuthorization(auth.Data).Get().SendAsync<JObject>().Result;
            if (!result.IsSuccess)
            {
                results.SetFailure(result.ErrorMessage + "System Error :  Colud not find exists data.");
                return results;
            }
            

            return results;
        }
    }
}
