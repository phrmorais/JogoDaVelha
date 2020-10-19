using JogoDaVelha.Biblioteca.Ultilidades;
using JogoDaVelha.Dominio.Enumerados;
using System;

namespace JogoDaVelha.Dominio.DTO
{
    public class MovimentoDTO
    {
        public Guid Id { get; set; }
        public string player { get; set; }
        public Position Position { get; set; }
        public Opcao Jogador { get => ConversorEnum.EnumItem<Opcao>(player); }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}