using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFYClient.Models
{
    internal class WorkingAccount
    {
        public int AFYWorkingAccountId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ShouldWorkingHours { get; set; }
        public decimal ShouldWorkingHoursFull { get; set; }
        public decimal WorkingHourPercentage { get; set; }
        public int HolydaysCount { get; set; }
        public virtual ICollection<WorkingTime> WorkingTimes { get; set; }
        public bool IsActive { get; set; }
    }
}
