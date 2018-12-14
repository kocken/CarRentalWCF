using System.Runtime.Serialization;

namespace Storage
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}] {FirstName} {LastName} {Email} {TelephoneNumber}";
        }

        public bool IsValid()
        {
            if (FirstName == null || LastName == null || TelephoneNumber == null || Email == null || Id < 0)
            {
                return false;
            }
            return true;
        }
    }
}