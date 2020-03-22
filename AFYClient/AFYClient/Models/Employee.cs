using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFYClient.Models
{
    internal class Employee
    {
        public int AFYEmployeeId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public virtual ICollection<WorkingAccount> Accounts { get; set; }
    }
}
