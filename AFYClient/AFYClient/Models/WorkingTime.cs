using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFYClient.Models
{
    internal class WorkingTime
    {
        public int AFYWorkingTimeId { get; set; }
        public int Week { get; set; }
        public DateTime WorkingDay { get; set; }
        public int Month { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HoursToWork { get; set; }
        public decimal Difference { get; set; }
        public WorkingAccount Account { get; set; }
    }
}
