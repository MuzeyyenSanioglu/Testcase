using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testcase.Appointments.API.DTOs;
using Testcase.Appointments.API.Services;
using Testcase.Appointments.Domain;
using Testcase.Appointments.Domain.Repositories;
using Testcase.Appointments.Domain.Response;

namespace Testcase.Appointments.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appoinmentRepository;
        private readonly IUserServices _userServices;

        public AppointmentsController(IMapper mapper, IAppointmentRepository appoinmentRepository, IUserServices userServices)
        {
            _mapper = mapper;
            _appoinmentRepository = appoinmentRepository;
            _userServices = userServices;
        }
        [ProducesResponseType(typeof(APIResponse<IEnumerable<AppoinmentsDto>>), StatusCodes.Status200OK)]
        [HttpGet("{userid}")]
        public IActionResult GetAppointmentsByUser(string userId)
        {
            APIResponse<IEnumerable<AppoinmentsDto>> result = new APIResponse<IEnumerable<AppoinmentsDto>>();
            APIResponse<IEnumerable<Appointment>> appoinments = _appoinmentRepository.GetAppointmentsWithuserId(userId).Result;
            if (!appoinments.IsSuccess)
                return NotFound(appoinments.ErrorMessage);
            result.SetData(_mapper.Map<IEnumerable<Appointment>, IEnumerable<AppoinmentsDto>>(appoinments.Data));
            return Ok(result);
        }
        [ProducesResponseType(typeof(APIResponse<AppoinmentsDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult CreateAppoinments(AppoinmentsDto appoinment)
        {
            APIResponse<AppoinmentsDto> result = new APIResponse<AppoinmentsDto>();
            var existUser = _userServices.GetUserById(appoinment.UserId);
            if (!existUser.IsSuccess)
                return Ok("User not found");
            DateTime endhour = appoinment.Date.AddHours(1);
            string timeSlot = appoinment.Date.ToString("HH:mm") + "-" + endhour.ToString("HH:mm");
            #region Check Appointment
            APIResponse existsAppoinment = _appoinmentRepository.CheckUserAppointment(appoinment.UserId, appoinment.Date, timeSlot).Result;
            if (existsAppoinment.AlreadyExist)
            {
                result.SetFailure("User have another appointment.");
                return Ok(result);
            }

            #endregion
            Appointment appointmentEntitiy = _mapper.Map<AppoinmentsDto, Appointment>(appoinment);
            appointmentEntitiy.Date = appoinment.Date;
            appointmentEntitiy.TimeSlot = timeSlot;
            APIResponse entityResponse = _appoinmentRepository.Create(appointmentEntitiy).Result;
            result.ObjectId = appointmentEntitiy.AppointmetsId;
            if (!entityResponse.IsSuccess)
                return NotFound(entityResponse.ErrorMessage);
            result.SetData(appoinment);
            return Ok(result);
        }
        [ProducesResponseType(typeof(APIResponse<IEnumerable<AppoinmentsDto>>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetAppoinments()
        {
            APIResponse<IEnumerable<AppoinmentsDto>> result = new APIResponse<IEnumerable<AppoinmentsDto>>();
            APIResponse<IEnumerable<Appointment>> appoinments = _appoinmentRepository.GetAll().Result;
            if (!appoinments.IsSuccess)
                return NotFound(appoinments.ErrorMessage);
            result.SetData(_mapper.Map<IEnumerable<Appointment>, IEnumerable<AppoinmentsDto>>(appoinments.Data));
            return Ok(result);

        }
    }
}
