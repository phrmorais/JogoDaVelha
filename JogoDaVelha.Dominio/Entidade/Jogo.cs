using JogoDaVelha.Dominio.Enumerados;
using System;

namespace JogoDaVelha.Dominio.Entidade
{
    public class Jogo
    {
        public Guid Id { get; set; }

        public Opcao[,] Tabuleiro => new Opcao[3, 3];
    }
}