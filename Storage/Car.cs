using System.Runtime.Serialization;

namespace Storage
{
    [DataContract]
    public class Car
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RegistrationNumber { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public int Year { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}] {Brand} {Model} {Year} {RegistrationNumber}";
        }
    }
}