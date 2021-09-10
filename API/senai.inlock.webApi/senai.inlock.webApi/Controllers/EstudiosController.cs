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
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _EstudioRepository { get; set; }

        public EstudiosController()
        {
            _EstudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudios = _EstudioRepository.ListarTodos();

            return Ok(listaEstudios);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudioDomain estudioBuscado = _EstudioRepository.BuscarPorID(id);

            if (estudioBuscado == null)
            {
                return NotFound("Nenhum estudio encontrado! Verifique se colocou a ID correta!");
            }

            return Ok(estudioBuscado);
        }

    }
}
