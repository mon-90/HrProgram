using System;
using System.Collections.Generic;

namespace HrProgram.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfRelease { get; set; } = null;

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int WorkplaceId { get; set; }
        public virtual Workplace Workplace { get; set; }

        public virtual IEnumerable<Vacation> Vacations { get; set; }
    }
}
