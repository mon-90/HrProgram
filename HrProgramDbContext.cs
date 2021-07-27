using HrProgram.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrProgram
{
    public class HrProgramDbContext : DbContext
    {
        public HrProgramDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region address
            var address = modelBuilder.Entity<Address>();

            address.Property(a => a.City)
                .IsRequired();

            address.Property(a => a.Street)
                .IsRequired();

            address.Property(a => a.HouseNumber)
                .IsRequired();

            address.Property(a => a.PostalCode)
                .IsRequired();
            #endregion

            #region contactPerson
            var contactPerson = modelBuilder.Entity<ContactPerson>();

            contactPerson.Property(c => c.FirstName)
                .IsRequired();

            contactPerson.Property(c => c.LastName)
                .IsRequired();
            #endregion

            #region user
            var user = modelBuilder.Entity<User>();

            user.Property(u => u.FirstName)
                .IsRequired();

            user.Property(u => u.LastName)
                .IsRequired();

            user.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(30);

            user.Property(u => u.PasswordHash)
                .IsRequired();

            user.Property(u => u.IdNumber)
                .IsRequired();

            user.Property(u => u.DateOfBirth)
                .IsRequired();

            user.Property(u => u.DateOfEmployment)
                .IsRequired();

            user.Property(u => u.AddressId)
                .IsRequired();

            user.Property(u => u.RoleId)
                .IsRequired();

            user.Property(u => u.WorkplaceId)
                .IsRequired();
            #endregion

            #region vacation
            var vacation = modelBuilder.Entity<Vacation>();

            vacation.Property(v => v.DayOfStart)
                .IsRequired();

            vacation.Property(v => v.DayOfEnd)
                .IsRequired();
            #endregion

            #region workplace
            var workplace = modelBuilder.Entity<Workplace>();

            workplace.Property(w => w.Name)
                .IsRequired();
            #endregion

            #region role
            var role = modelBuilder.Entity<Role>();

            role.Property(r => r.Name)
                .IsRequired();
            #endregion
        }
    }
}
