using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFYAPI.Models
{
    public class AFYHoliday
    {
        public int AFYHolidayId { get; set; }
        public DateTime Holiday { get; set; }
        public string HolidayName { get; set; }
    }
}
