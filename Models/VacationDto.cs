using System;

namespace HrProgram.Models
{
    public class VacationDto
    {
        public DateTime? DayOfStart { get; set; }
        public DateTime? DayOfEnd { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
