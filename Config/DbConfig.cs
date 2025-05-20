using EliminIQ_TCC.Models;
using Microsoft.EntityFrameworkCore;

namespace EliminIQ_TCC.Config
{
    public class DbConfig : DbContext
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options) { }

        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Quiz> Quiz { get; set; }

    }
}
