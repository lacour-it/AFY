using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFYAPI.Models
{
    public class AFYWorkingTime
    {
        public int AFYWorkingTimeId { get; set; }
        public int Week { get; set; }
        public DateTime WorkingDay { get; set; }
        public int Month { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HoursToWork { get; set; }
        public decimal Difference { get; set; }
        public AFYWorkingAccount Account { get; set; }
    }
}
