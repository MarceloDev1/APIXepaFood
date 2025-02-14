using Microsoft.AspNetCore.Mvc;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using System.Text.RegularExpressions;

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

            var erroValidacao = novoUsuario?.ValidarCamposObrigatorios();
            if (erroValidacao != null)
                return BadRequest(erroValidacao);


            var emailValidacao = novoUsuario?.ValidarEmail(novoUsuario.Email);
            if (emailValidacao != null)
                return BadRequest(emailValidacao);

            var senhaValidacao = novoUsuario?.ValidarSenha(novoUsuario.Senha);
            if(senhaValidacao != null)
              return BadRequest(senhaValidacao);

            if (_usuarioServico.ObterUsuarioPorEmail(novoUsuario.Email) != null)
                return Conflict("Já existe um usuário com este email.");

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

            return Ok(new
            {
                Message = "Login efetuado com sucesso!",
                Usuario = usuario
            });
        }
    }
}