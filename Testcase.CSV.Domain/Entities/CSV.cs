using Newtonsoft.Json.Linq;

namespace Testcase.CSV.Domain
{
    public class CSV
    {
        public string id { get; set; }
        public User user{ get; set; }
    }
    public class User
    {
        public string UserId { get; set; }
        public Appointment appoinments { get; set; }


    }

    public class Appointment
    {
        public string  appoinmentId { get; set; }
        public Test tests { get; set; }
    }

    public class Test
    {

        public string  TestId { get; set; }
        public string Datas { get; set; }
    }
}