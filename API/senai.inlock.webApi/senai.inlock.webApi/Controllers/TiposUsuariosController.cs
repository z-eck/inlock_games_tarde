using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
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
    public class TiposUsuariosController : ControllerBase
    {
        private ITipoUsuarioRepository tipoUsuarioRepositorio;
        public TiposUsuariosController()
        {
            tipoUsuarioRepositorio = new TipoUsuarioRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<TipoUsuarioDomain> tiposUsuarios = tipoUsuarioRepositorio.listarTodos();
            return Ok(tiposUsuarios);
        }
    }
}
