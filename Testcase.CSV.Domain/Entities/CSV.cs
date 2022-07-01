using Newtonsoft.Json.Linq;

namespace Testcase.CSV.Domain
{
    public class CSV
    {
        public string id { get; set; }
        public List<User> users{ get; set; }
    }
    public class User
    {
        public string UserId { get; set; }
        public List<Appointment> appoinments { get; set; }


    }

    public class Appointment
    {
        public string  appoinmentId { get; set; }
        public List<Test> tests { get; set; }
    }

    public class Test
    {

        public string  TestId { get; set; }
        public JObject Datas { get; set; }
    }
}