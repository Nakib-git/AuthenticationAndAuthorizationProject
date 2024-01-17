using Microsoft.EntityFrameworkCore;
using Register.Domain.Models;

namespace Register.Infrastructure.AppContext {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base(options) {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
