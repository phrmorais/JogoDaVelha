using JogoDaVelha.Dominio.Contrato;
using JogoDaVelha.Dominio.Entidade;
using JogoDaVelha.Dominio.Enumerados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JogoDaVelha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IBaseRepositorio<Jogo> _jogo;

        public GamesController(IBaseRepositorio<Jogo> jogo)
        {
            _jogo = jogo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetGames() { return Ok($"Jogo estão ativos:{_jogo.Where(c => c.Status != Status.Aberto && c.Status != Status.FimDaPartida).Count()}"); }

    }
}
