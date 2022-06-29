using System.ComponentModel.DataAnnotations;

namespace Testcase.Appointments.API.DTOs
{
    public class AppoinmentsDto
    {
        public string AppoinmentName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime Date { get; set; }

        [Range(0, 24, ErrorMessage = "The hour {0} must be greater than {1}.")]
        public int StartHour { get; set; }
    }
}
