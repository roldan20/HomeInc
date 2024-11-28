using Microsoft.EntityFrameworkCore;
using HomeInc.Domain.Entities;

namespace HomeInc.Ifrastructura
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }
    }
}
