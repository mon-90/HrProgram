using System;

namespace HrProgram.Models
{
    public class UpdateDataUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; } = null;
        public DateTime? DateOfEmployment { get; set; } = null;
        public DateTime? DateOfRelease { get; set; } = null;

        public int? RoleId { get; set; }
        public int? WorkplaceId { get; set; }

        // address
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        // contact person
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
    }
}
