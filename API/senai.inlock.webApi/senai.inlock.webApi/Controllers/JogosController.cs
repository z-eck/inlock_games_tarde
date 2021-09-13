using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domain;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class JogosController : ControllerBase
    {
        private IJogoRepository _JogoRepository { get; set; }

        public JogosController()
        {
            _JogoRepository = new JogoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<JogoDomain> listaJogos = _JogoRepository.ListarTodos();

            return Ok(listaJogos);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {
            _JogoRepository.Cadastrar(novoJogo);

            return StatusCode(201);
        }

        [Authorize(Roles = "ADMINISTRADOR, CLIENTE")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogoDomain jogoBuscado = _JogoRepository.BuscarPorID(id);

            if (jogoBuscado == null)
            {
                return NotFound("Nenhum jogo encontrado! Verifique se colocou a ID correta!");
            }

            return Ok(jogoBuscado);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(JogoDomain JogoAtualizado, int idJogoAtualizado)
        {
            JogoDomain jogoAtualizar = _JogoRepository.BuscarPorID(idJogoAtualizado);

            if (jogoAtualizar == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Jogo não encontrado! Verifique se a ID inclusa é válida!",
                            erro = true
                        }
                    );
            }

            try
            {
                _JogoRepository.Atualizar(JogoAtualizado, idJogoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
        
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _JogoRepository.Deletar(id);

            return NoContent();
        }

    }
}
