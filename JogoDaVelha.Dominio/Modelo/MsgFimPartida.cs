using JogoDaVelha.Biblioteca.Ultilidades;
using JogoDaVelha.Dominio.Enumerados;

namespace JogoDaVelha.Dominio.Modelo
{
    public class MsgFimPartida
    {
        public MsgFimPartida(string msg, Opcao winner)
        {
            Msg = msg;
            Winner = winner == Opcao.Empate ? "Draw" : ConversorEnum.EnumItemString(Winner);
        }

        public string Msg { get; private set; }
        public string Winner { get; private set; }
    }
}