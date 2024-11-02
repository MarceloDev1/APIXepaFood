using Microsoft.AspNetCore.Mvc;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Servicos;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly LogMongoService _logMongoService;
        public UsuarioController(IUsuarioServico usuarioServico, LogMongoService logMongoService)
        {
            _usuarioServico = usuarioServico;
            _logMongoService = logMongoService;
        }

        [HttpPost]
        [Route("CriarUsuario")]
        public IActionResult CriarUsuario([FromBody] UsuarioRequest novoUsuario)
        {
            try
            {
                if (novoUsuario == null)
                    return BadRequest("Dados inválidos.");

                var usuarioExistente = _usuarioServico.ObterUsuarioPorEmail(novoUsuario.Email);
                if (usuarioExistente != null)
                    return Conflict("Usuário com este email já existe.");

                _usuarioServico.CriarUsuario(novoUsuario);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Usuário criado com sucesso: Usuario: {novoUsuario.Nome}, Telefone: {novoUsuario.Telefone}, Localização: {novoUsuario.Localizacao}",
                    Source = nameof(CriarUsuario),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Usuário criado com sucesso!", usuario = novoUsuario });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(CriarUsuario),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao criar usuário");
            }
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
            try
            {
                var usuarioExistente = _usuarioServico.ObterUsuarioPorId(novoUsuario.IdUsuario);

                if (usuarioExistente == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                _usuarioServico.AtualizarUsuarioPorId(novoUsuario);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Usuário atualizado com sucesso: Usuario: {novoUsuario.Nome}, Telefone: {novoUsuario.Telefone}, Localização: {novoUsuario.Localizacao}",
                    Source = nameof(AtualizarUsuarioPorId),
                    StackTrace = null
                });

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(AtualizarUsuarioPorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao atualizar usuário");
            }
        }

        [HttpDelete]
        [Route("DeletarUsuarioPorId/{idUsuario}")]
        public IActionResult DeletarUsuarioPorId(int idUsuario)
        {
            try
            {
                var usuarioExistente = _usuarioServico.ObterUsuarioPorId(idUsuario);

                if (usuarioExistente == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                _usuarioServico.DeletarUsuarioPorId(idUsuario);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Usuário deletado com sucesso: IdUsuario: {usuarioExistente.IdUsuario}, Usuario: {usuarioExistente.Nome}, Telefone: {usuarioExistente.Telefone}, Localização: {usuarioExistente.Localizacao}",
                    Source = nameof(DeletarUsuarioPorId),
                    StackTrace = null
                });

                return Ok("Usuário deletado com sucesso.");
            }
            catch(Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(DeletarUsuarioPorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao deletar usuário");
            }
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