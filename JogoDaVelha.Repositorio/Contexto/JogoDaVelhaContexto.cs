using JogoDaVelha.Dominio.Entidade;
using Microsoft.EntityFrameworkCore;

namespace JogoDaVelha.Repositorio.Contexto
{
    public class JogoDaVelhaContexto : DbContext
    {
        public JogoDaVelhaContexto(DbContextOptions<JogoDaVelhaContexto> options)
          : base(options)
        { }

        public DbSet<Jogo> Jogos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Config.JogoConfiguracao());

            base.OnModelCreating(modelBuilder);
        }
    }
}