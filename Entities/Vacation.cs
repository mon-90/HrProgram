using System;

namespace HrProgram.Entities
{
    public class Vacation
    {
        public int Id { get; set; }
        public DateTime? DayOfStart { get; set; } = null;
        public DateTime? DayOfEnd { get; set; } = null;
        public bool? IsAccepted { get; set; } = null;
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
