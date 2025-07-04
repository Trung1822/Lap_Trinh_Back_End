using Microsoft.EntityFrameworkCore;
using BTL1.Models;

namespace BTL1.Data
{
    public class BTLDbContext : DbContext
    {
        public BTLDbContext(DbContextOptions<BTLDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
