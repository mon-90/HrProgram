namespace HrProgram.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public int? ContactPersonId { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
    }
}
