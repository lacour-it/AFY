using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFYAPI.Models
{
    public class AFYUserRole
    {
        public int AFYUserRoleId { get; set; }
        public AFYUser User { get; set; }
        public AFYRole Role { get; set; }
    }
}
