
using Microsoft.EntityFrameworkCore;
using RepeatClassApi.Entities;

namespace RepeatClassApi.Data
{
    public class RepeatClassDbContext : DbContext
    {

        public RepeatClassDbContext(DbContextOptions<RepeatClassDbContext> option) : base(option)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
