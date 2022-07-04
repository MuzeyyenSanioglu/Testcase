using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Testcase.Appointments.API.DTOs
{
    public class AppoinmentsDto
    {
        public string AppoinmentName { get; set; } = null!;
        public string UserId { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm:ss}")]
        public DateTime AppoinmentStartDate { get; set; }
    }
}
