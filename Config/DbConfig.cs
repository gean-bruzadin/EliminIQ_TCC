using EliminIQ_TCC.Models;
using Microsoft.EntityFrameworkCore;

namespace EliminIQ_TCC.Config
{
    public class DbConfig : DbContext
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Quizz> Quizz { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Alternativa> Alternativa { get; set; }
        public DbSet<Privacidade> Privacidade { get; set; }
        public DbSet<Dificuldade> Dificuldade { get; set; }
        public DbSet<TipoQuizz> TipoQuizz { get; set; }
        public DbSet<Usuario_Quizz> Usuario_Quizz { get; set; }
        public DbSet<Criar_Quizz> Criar_Quizz { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario_Quizz>().HasKey(uq => new { uq.Fk_Usuario, uq.Fk_Quizz });
            modelBuilder.Entity<Criar_Quizz>().HasKey(cq => new { cq.Fk_Usuario, cq.Fk_Quizz });
        }

    }
}
