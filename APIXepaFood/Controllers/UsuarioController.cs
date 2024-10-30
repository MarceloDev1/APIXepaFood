using Microsoft.AspNetCore.Mvc;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IConfiguration _configuration;
        public UsuarioController(IUsuarioServico usuarioServico, IConfiguration configuration)
        {
            _usuarioServico = usuarioServico;
            _configuration = configuration;
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

        //TODO: Verificar porque precisa desse endpoint - vulnerabilidade.
        [HttpGet]
        [Route("RetornarUsuarios")]
        public List<Usuario> RetornarUsuarios()
        {
            var usuarios = _usuarioServico.ObterTodosUsuarios();
            return usuarios;
        }

        //TODO: Verificar porque precisa desse endpoint - vulnerabilidade.
        [HttpGet]
        [Route("RetornarUsuariosPorId")]
        public Usuario RetornarUsuariosPorId(int idUsuario)
        {
            var usuario = _usuarioServico.ObterUsuarioPorId(idUsuario);
            return usuario;
        }

        [Authorize]
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

        [Authorize]
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

            var token = GenerateToken(loginRequest.Email);
            return Ok(new { token });
        }

        private string GenerateToken(string userEmail)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(_configuration["JwtSettings:TokenExpiryHours"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}