using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        IUsuarioRepository usuarioRepositorio;
        public UsuariosController()
        {
            usuarioRepositorio = new UsuarioRepository();
        }
        [HttpPost("Login")]
        public IActionResult Login(UsuarioDomain login)
        {
                UsuarioDomain usuarioLogin = usuarioRepositorio.logar(login.email, login.senha);
                if (usuarioLogin == null)
                {
                    return NotFound("Email ou senha inválidos!");
                }
                else
                {
                    var claimsToken = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioLogin.email),
                    new Claim(JwtRegisteredClaimNames.NameId, usuarioLogin.idUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioLogin.tipoUsuario.titulo)
                };

                    var chaveToken = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chaveSegurancaInlock"));

                    var credenciaisToken = new SigningCredentials(chaveToken, SecurityAlgorithms.HmacSha256);

                    var loginToken = new JwtSecurityToken(
                            issuer: "senai.inlock.webAPI",
                            audience: "senai.inlock.webAPI",
                            claims: claimsToken,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: credenciaisToken
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(loginToken)
                    });
                }
        }
        [HttpGet]
        public IActionResult LerTodos()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();
            return Ok(usuarios);
        }

        [HttpGet("{idUsuarioBuscado}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult BuscarPorId(int idUsuarioBuscado)
        {
            UsuarioDomain usuarioBuscado = usuarioRepositorio.buscarPorID(idUsuarioBuscado);
            if (usuarioBuscado != null)
            {
                return Ok(usuarioBuscado);
            }
            else return NotFound("Id de usuário inválido!!!");
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Cadastrar(UsuarioDomain novoUsuario)
        {
            TipoUsuarioRepository tipoUsuarioRepositorio = new TipoUsuarioRepository();
            if (tipoUsuarioRepositorio.buscarPorId(novoUsuario.idTipoUsuario) == null)
            {
                return BadRequest("Id do tipo de usuário deve ser especificado!");
            }
            else if (novoUsuario.email == null)
            {
                return BadRequest("Email deve ser especificado!");
            }
            else if (novoUsuario.senha == null)
            {
                return BadRequest("Senha deve ser especificada!");
            }
            else
            {
                usuarioRepositorio.cadastrar(novoUsuario);
                return StatusCode(201);
            }
        }

        [HttpPut("{idUsuarioAtualizado}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Atualizar(UsuarioDomain usuarioAtualizado, int idUsuarioAtualizado)
        {
            if (usuarioRepositorio.buscarPorID(idUsuarioAtualizado) != null)
            {
                TipoUsuarioRepository tipoUsuarioRepositorio = new TipoUsuarioRepository();
                if (tipoUsuarioRepositorio.buscarPorId(usuarioAtualizado.idTipoUsuario) == null)
                {
                    return NotFound("Um id do tipo de usuário válido deve ser especificado!");
                }
                else if (usuarioAtualizado.email == null)
                {
                    return BadRequest("Email deve ser especificado!");
                }
                else if (usuarioAtualizado.senha == null)
                {
                    return BadRequest("Senha deve ser especificada!");
                }
                else
                {
                    usuarioRepositorio.atualizar(usuarioAtualizado, idUsuarioAtualizado);
                    return NoContent();
                }
            }
            else return NotFound("Id de usuário inválido!");
        }

        [HttpDelete("{idUsuarioDeletado}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Deletar(int idUsuarioDeletado)
        {
            if (usuarioRepositorio.buscarPorID(idUsuarioDeletado) != null)
            {
                usuarioRepositorio.deletar(idUsuarioDeletado);
                return NoContent();
            }
            else
            {
                return NotFound("Id de usuário inválido!");
            }
        }
    }
}