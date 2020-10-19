using JogoDaVelha.Dominio.Entidade;
using JogoDaVelha.Dominio.Enumerados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JogoDaVelha.Repositorio.Config
{
    public class JogoConfiguracao : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder
              .HasKey(c => c.Id);

            builder.Property(c => c.Tabuleiro).HasConversion(v => JsonConvert.SerializeObject(v),
        v => JsonConvert.DeserializeObject<List<Opcao>>(v).ToArray());
        }
    }
}