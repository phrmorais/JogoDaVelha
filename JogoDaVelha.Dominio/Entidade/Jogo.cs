using JogoDaVelha.Dominio.Enumerados;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace JogoDaVelha.Dominio.Entidade
{
    public class Jogo : Entity
    {
        public Opcao[] Tabuleiro { get; set; }

        [NotMapped]
        private Opcao[,] _tabuleiro
        {
            get
            {
                var tabuleiro = new Opcao[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var idx = (i * 3) + j;
                        tabuleiro[i, j] = Tabuleiro.Length < idx ? Opcao.Vazio : Tabuleiro[idx];
                    }
                }

                return tabuleiro;
            }
            set { Tabuleiro = value.OfType<Opcao>().ToArray(); }
        }

        public Opcao PrimeiroJogador { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Inicia uma nova instacia do jogo
        /// </summary>
        public Jogo() : base(default)
        {
            PrimeiroJogador = DateTime.Now.Second % 2 == 0 ? Opcao.O : Opcao.X;
            Status = Status.Aberto;
            _tabuleiro = new Opcao[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _tabuleiro[i, j] = Opcao.Vazio;
                }
            }
        }

        /// <summary>
        /// Executa o movimento para da opção enviada, retorna se movimento pode ser executado
        /// </summary>
        /// <param name="linha"></param>
        /// <param name="coluna"></param>
        /// <param name="opcao"></param>
        /// <returns></returns>
        public bool Movimento(int linha, int coluna, Opcao opcao)
        {
            var idx = coluna + (linha * 3);
            if (Tabuleiro[idx] != Opcao.Vazio && opcao != Opcao.Vazio)
            {
                return false;
            }
            Tabuleiro[idx] = opcao;
            return true;
        }

        /// <summary>
        /// Mostra o tabuleiro do jogo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Append(" ").Append(_tabuleiro[i, j]);

                    if (j < 2)
                    {
                        sb.Append(" | ");
                    }
                }
                sb.AppendLine();

                if (i < 2)
                {
                    sb.Append("------------").AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Verifica se existe vencedor por linhas
        /// </summary>
        /// <returns></returns>
        private Opcao? CheckLinhas()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_tabuleiro[i, 0] == Opcao.Vazio) continue;
                if (_tabuleiro[i, 0] == _tabuleiro[i, 1] && _tabuleiro[i, 1] == _tabuleiro[i, 2])
                    return _tabuleiro[i, 0];
            }

            return null;
        }

        /// <summary>
        /// Verifica se existe vencedor por colunas
        /// </summary>
        /// <returns></returns>
        private Opcao? CheckColunas()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_tabuleiro[0, i] == Opcao.Vazio) continue;
                if (_tabuleiro[0, i] == _tabuleiro[1, i] && _tabuleiro[1, i] == _tabuleiro[2, i])
                    return _tabuleiro[0, i];
            }

            return null;
        }

        /// <summary>
        /// Verifica se existe vencedor na horizontal
        /// </summary>
        /// <returns></returns>
        private Opcao? CheckHorizontal()
        {
            if (_tabuleiro[0, 0] != Opcao.Vazio && _tabuleiro[0, 0] == _tabuleiro[1, 1] && _tabuleiro[1, 1] == _tabuleiro[2, 2])
            {
                return _tabuleiro[0, 0];
            }

            if (_tabuleiro[0, 2] != Opcao.Vazio && _tabuleiro[0, 2] == _tabuleiro[1, 1] && _tabuleiro[1, 1] == _tabuleiro[2, 0])
            {
                return _tabuleiro[0, 2];
            }

            return null;
        }

        /// <summary>
        /// Verifica se deu empate
        /// </summary>
        /// <returns></returns>
        private Opcao? CheckEmpate()
        {
            if (!Tabuleiro.Any(c => c == Opcao.Vazio))
                return Opcao.Empate;
            return null;
        }

        public Opcao? VerificaVencedor()
        {
            return CheckColunas() ?? CheckHorizontal() ?? CheckLinhas() ?? CheckEmpate();
        }
    }
}