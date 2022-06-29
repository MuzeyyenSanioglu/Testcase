using MongoDB.Bson.Serialization.Attributes;

namespace Testcase.Appointments.Domain
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string AppointmetsId { get; set; } = null!;
        public string AppoinmentName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = null!;
    }
}