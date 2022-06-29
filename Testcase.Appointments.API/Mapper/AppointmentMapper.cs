using AutoMapper;
using Testcase.Appointments.API.DTOs;
using Testcase.Appointments.Domain;

namespace Testcase.Appointments.API.Mapper
{
    public class AppointmentMapper :Profile
    {
        public AppointmentMapper()
        {
            CreateMap<Appointment, AppoinmentsDto>().ReverseMap();
        }
    }
}
