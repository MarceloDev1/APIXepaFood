using Microsoft.AspNetCore.Mvc;
using APIXepaFood.Models;
using APIXepaFood.Interfaces;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        [Route("CriarUsuario")]
        public IActionResult CriarUsuario([FromBody] Usuario novoUsuario)
        {
            //if (novoUsuario == null)
            //    return BadRequest("Dados inválidos.");

            //var usuarioExistente = _usuarioRepositorio.ObterUsuarioPorEmail(novoUsuario.Email);
            //if (usuarioExistente != null)
            //    return Conflict("Usuário com este email já existe.");

            _usuarioRepositorio.CriarUsuario(novoUsuario);
            return Ok(new { mensagem = "Usuário criado com sucesso!", usuario = novoUsuario });
        }

        [HttpGet]
        [Route("RetornarUsuarios")]
        public List<Usuario> RetornarUsuarios()
        {
            var clientes = _usuarioRepositorio.ObterTodosUsuarios();
            return clientes;
        }

        [HttpGet]
        [Route("RetornarUsuariosPorId")]
        public Usuario RetornarUsuariosPorId(int idUsuario)
        {          
            var usuario = _usuarioRepositorio.ObterUsuarioPorId(idUsuario);
            return usuario;
        }
    }
}