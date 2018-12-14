using System;
using System.Runtime.Serialization;

namespace Storage
{
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Car Car { get; set; }

        [DataMember]
        public Customer Customer { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public DateTime ReturnTime { get; set; }

        public override string ToString()
        {
            return (ReturnTime != default(DateTime) ? "[RETURNED]" : "[ACTIVE]") +
                $" [Id: {Id}] {Customer.FirstName} {Customer.LastName} " +
                $"{StartTime}-{EndTime} with car {Car.ToString()}";
        }

        public bool IsStarted()
        {
            return DateTime.Now >= StartTime;
        }
    }
}