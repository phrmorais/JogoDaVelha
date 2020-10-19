using JogoDaVelha.Dominio.Contrato;
using JogoDaVelha.Dominio.DTO;
using JogoDaVelha.Dominio.Entidade;
using JogoDaVelha.Dominio.Enumerados;
using JogoDaVelha.Dominio.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JogoDaVelha.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private readonly IBaseRepositorio<Jogo> _jogo;

        public GameController(IBaseRepositorio<Jogo> jogo)
        {
            _jogo = jogo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get() { return Ok($"Jogo estão ativos:{_jogo.GetAll().Count()}"); }

        [HttpPost]
        public IActionResult Post()
        {
            lock (this)
            {
                var jogoaberto = _jogo.Where(c => c.Status == Status.Aberto).FirstOrDefault();
                Opcao Jogador;
                if (jogoaberto == null)
                {
                    jogoaberto = new Jogo();
                    Jogador = jogoaberto.PrimeiroJogador;
                }
                else
                {
                    jogoaberto.Status = jogoaberto.PrimeiroJogador == Opcao.O ? Status.VezDeO : Status.VezDeX;
                    Jogador = jogoaberto.PrimeiroJogador == Opcao.O ? Opcao.X : Opcao.O;
                }

                _jogo.Save(jogoaberto);

                return Ok(new
                {
                    jogoaberto.Id,
                    firstPlayer = jogoaberto.PrimeiroJogador.ToString(),
                    YourPlayer = Jogador.ToString()
                });
            }
        }

        [HttpPost]
        [Route("{id}/movement")]
        public async Task<IActionResult> movement(Guid id, [FromBody] MovimentoDTO movimento)
        {
            if (movimento == null) return BadRequest(new MsgResponse("Movimento não informado"));
            if (movimento.Position.X < 0 || movimento.Position.X > 2 || movimento.Position.Y < 0 || movimento.Position.Y > 2)
            {
                return BadRequest(new MsgResponse("Movimento fora do tabuleiro"));
            }
            if (id != movimento.Id)
                return BadRequest(new MsgResponse("Id do movimento diferente do Id da URI"));

            var jogoAtual = _jogo.GetId(id);
            if (jogoAtual == null)
                return BadRequest(new MsgResponse("Partida não encontrada"));

            if (jogoAtual.Status == Status.Aberto)
                return BadRequest(new MsgResponse("Partida aguardando segundo jogador"));

            if (jogoAtual.Status == Status.FimDaPartida)
                return BadRequest(new MsgResponse("Partida não encontrada"));

            if (jogoAtual.Status == Status.VezDeO && movimento.Jogador == Opcao.X || jogoAtual.Status == Status.VezDeX && movimento.Jogador == Opcao.O)
                return BadRequest(new MsgResponse("Não é turno do jogador"));

            if (!jogoAtual.Movimento(movimento.Position.X, movimento.Position.Y, movimento.Jogador))
                return BadRequest(new MsgResponse("Movimento já executado"));

            jogoAtual.Status = jogoAtual.Status == Status.VezDeO ? Status.VezDeX : Status.VezDeO;

            var vencedor = jogoAtual.VerificaVencedor();

            if (vencedor != null)
                jogoAtual.Status = Status.FimDaPartida;

            await _jogo.Save(jogoAtual);

            if (jogoAtual.Status == Status.FimDaPartida)
                return Ok(new MsgFimPartida("Partida finalizada", vencedor.GetValueOrDefault()));
            return Ok();
        }
    }
}