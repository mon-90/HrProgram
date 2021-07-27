using System.Collections.Generic;

namespace HrProgram.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}
