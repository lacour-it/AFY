using Microsoft.EntityFrameworkCore;
using AFYAPI.Models;

namespace AFYAPI.Data
{
    public class AFYContext : DbContext
    {
        public AFYContext(DbContextOptions<AFYContext> options) : base(options)
        {
        }
        public DbSet<AFYUser> Users { get; set; }
        public DbSet<AFYRole> Roles { get; set; }
        public DbSet<AFYUserRole> UserRoles { get; set; }
        public DbSet<AFYAPI.Models.AFYEmployee> AFYEmployee { get; set; }
        public DbSet<AFYAPI.Models.AFYHoliday> AFYHoliday { get; set; }
        public DbSet<AFYAPI.Models.AFYWorkingAccount> AFYWorkingAccount { get; set; }
        public DbSet<AFYAPI.Models.AFYWorkingTime> AFYWorkingTime { get; set; }
    }
}
