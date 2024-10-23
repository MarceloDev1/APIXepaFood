using Microsoft.AspNetCore.Mvc;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpPost]
        [Route("CriarUsuario")]
        public IActionResult CriarUsuario([FromBody] UsuarioRequest novoUsuario)
        {
            if (novoUsuario == null)
                return BadRequest("Dados inválidos.");

            var usuarioExistente = _usuarioServico.ObterUsuarioPorEmail(novoUsuario.Email);
            if (usuarioExistente != null)
                return Conflict("Usuário com este email já existe.");

            _usuarioServico.CriarUsuario(novoUsuario);
            return Ok(new { mensagem = "Usuário criado com sucesso!", usuario = novoUsuario });
        }

        [HttpGet]
        [Route("RetornarUsuarios")]
        public List<Usuario> RetornarUsuarios()
        {
            var usuarios = _usuarioServico.ObterTodosUsuarios();
            return usuarios;
        }

        [HttpGet]
        [Route("RetornarUsuariosPorId")]
        public Usuario RetornarUsuariosPorId(int idUsuario)
        {
            var usuario = _usuarioServico.ObterUsuarioPorId(idUsuario);
            return usuario;
        }

        [HttpPut]
        [Route("AtualizarUsuarioPorId")]
        public IActionResult AtualizarUsuarioPorId([FromBody] Usuario novoUsuario)
        {
            var usuarioExistente = _usuarioServico.ObterUsuarioPorId(novoUsuario.IdUsuario);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _usuarioServico.AtualizarUsuarioPorId(novoUsuario);

            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete]
        [Route("DeletarUsuarioPorId/{idUsuario}")]
        public IActionResult DeletarUsuarioPorId(int idUsuario)
        {
            var usuarioExistente = _usuarioServico.ObterUsuarioPorId(idUsuario);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _usuarioServico.DeletarUsuarioPorId(idUsuario);

            return Ok("Usuário deletado com sucesso.");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Senha))
                return BadRequest("Email ou senha não podem estar vazios.");

            var usuario = _usuarioServico.ObterUsuarioPorEmailSenha(loginRequest.Email, loginRequest.Senha);

            if (usuario == null)
                return BadRequest("Email ou senha inválidos.");

            return Ok("Login efetuado com sucesso !");
        }

    }
}