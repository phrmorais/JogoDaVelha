using JogoDaVelha.Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JogoDaVelha.Repositorio.Config
{
    public class JogoConfiguracao : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder
              .HasKey(c => c.Id);
        }
    }
}