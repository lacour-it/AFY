using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFYAPI.Models
{
    public class AFYWorkingAccount
    {
        public int AFYWorkingAccountId { get; set; }
        public AFYEmployee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ShouldWorkingHours { get; set; }
        public decimal ShouldWorkingHoursFull { get; set; }
        public decimal WorkingHourPercentage { get; set; }
        public int HolydaysCount { get; set; }
        public virtual ICollection<AFYWorkingTime> WorkingTimes { get; set; }
        public bool IsActive { get; set; }
    }
}
