using HrProgram.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HrProgram
{
    public class HrProgramSeeder
    {
        private readonly HrProgramDbContext _dbContext;

        public HrProgramSeeder(HrProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedRoles()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void SeedWorkplaces()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Workplaces.Any())
                {
                    var workplaces = GetWorkplaces();
                    _dbContext.Workplaces.AddRange(workplaces);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "Admin"
                    },
                    new Role()
                    {
                        Name = "Hr"
                    },
                    new Role()
                    {
                        Name = "Employee"
                    },
                    new Role()
                    {
                        Name = "Inactive"
                    }
                };

            return roles;
        }

        private IEnumerable<Workplace> GetWorkplaces()
        {
            var workplaces = new List<Workplace>()
                {
                    new Workplace()
                    {
                        Name = "Prezes"
                    },
                    new Workplace()
                    {
                        Name = "Manager"
                    },
                    new Workplace()
                    {
                        Name = "Starszy specjalista"
                    },
                    new Workplace()
                    {
                        Name = "Młodszy specjalista"
                    }
                };

            return workplaces;
        }
    }
}
