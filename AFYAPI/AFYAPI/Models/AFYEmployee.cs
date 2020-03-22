using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFYAPI.Models
{
    public class AFYEmployee
    {
        public int AFYEmployeeId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public virtual ICollection<AFYWorkingAccount> Accounts { get; set; }
    }
}
