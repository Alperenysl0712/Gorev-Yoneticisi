using Görev_Yöneticisi.Models;
using Microsoft.EntityFrameworkCore;

namespace Görev_Yöneticisi.DatabaseControl
{
    public class GorevYoneticisiContext : DbContext
    {
        public GorevYoneticisiContext(DbContextOptions<GorevYoneticisiContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Reports> Reports { get; set; }

    }
}
