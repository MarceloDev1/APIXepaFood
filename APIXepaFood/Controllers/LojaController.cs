using Domain.Interfaces;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Domain.Requests;
using Domain.Servicos;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : Controller
    {
        private readonly ILojaServico _lojaServico;
        private readonly LogMongoService _logMongoService;
        public LojaController(ILojaServico lojaServico, LogMongoService logMongoService)
        {
            _lojaServico = lojaServico;
            _logMongoService = logMongoService;
        }

        [HttpPost]
        [Route("CriarLoja")]
        public IActionResult CriarLoja([FromBody] LojaRequest novaLoja)
        {
            try
            {
                if (novaLoja == null)
                    return BadRequest("Dados inválidos para o cadastro.");

                _lojaServico.CriarLoja(novaLoja);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Loja criada com sucesso: {novaLoja.NomeLoja}, Endereço: {novaLoja.Localizacao}, Usuário: {novaLoja.IdUsuario}",
                    Source = nameof(CriarLoja),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Loja criada com sucesso!" });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(CriarLoja),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao Registrar loja");
            }
        }

        [HttpPost]
        [Route("CriarLojaEUsuario")]
        public IActionResult CriarLojaEUsuario([FromBody] LojaEUsuarioRequest novaLoja)
        {
            try
            {
                if (novaLoja == null)
                    return BadRequest("Dados inválidos para o cadastro.");

                _lojaServico.CriarLojaEUsuario(novaLoja);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Loja e usuário criados com sucesso: {novaLoja.NomeLoja}, Endereço: {novaLoja.LocalizacaoLoja}," +
                    $"Usuário: {novaLoja.Nome}, Email: {novaLoja.Email}, Telefone: {novaLoja.Telefone}",
                    Source = nameof(CriarLojaEUsuario),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Loja criada com sucesso!" });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(CriarLojaEUsuario),
                    StackTrace = ex.StackTrace
                });

                return StatusCode(500, "Erro ao registrar loja e usuário");
            }
        }

        [HttpGet]
        [Route("RetornarLojas")]
        public List<Loja> RetornarLojas()
        {
            var lojas = _lojaServico.ObterTodasLojas();
            return lojas;
        }

        [HttpGet]
        [Route("RetornarLojaPorId")]
        public IActionResult RetornarLojaPorId(int idLoja)
        {
            var loja = _lojaServico.ObterLojaPorId(idLoja);
            if (loja != null)
                return Ok(loja);

            return NotFound("Loja não encontrada");
        }

        [HttpGet]
        [Route("RetornarLojaPorIdUsuario")]
        public List<Loja> RetornarLojaPorIdUsuario(int idUsuario)
        {
            var lojas = _lojaServico.ObterLojaPorIdUsuario(idUsuario);
            return lojas;
        }

        [HttpPost]
        [Route("AtualizarLoja")]
        public IActionResult AtualizarLoja([FromBody] Loja loja)
        {
            try
            {
                var lojaExistente = _lojaServico.ObterLojaPorId(loja.IdLoja);
                if (lojaExistente == null)
                    return NotFound("Loja não encontrada! ");

                _lojaServico.AtualizarLoja(loja);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Loja atualizada com sucesso: {loja.NomeLoja}, Endereço: {loja.Localizacao}",
                    Source = nameof(AtualizarLoja),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Loja atualizada com sucesso!" });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(AtualizarLoja),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao atualizar loja.");
            }
        }

        [HttpPost]
        [Route("ExcluirLoja")]
        public IActionResult DeletarLoja(int idLoja)
        {
            try
            {
                var loja = _lojaServico.ObterLojaPorId(idLoja);

                if (loja != null)
                {
                    _lojaServico.DeletarLoja(idLoja);

                    _logMongoService.LogAsync(new LogMongo
                    {
                        Level = "Info",
                        Message = $"Loja excluída com sucesso: IdLoja: {loja.IdLoja}, NomeLoja: {loja.NomeLoja}",
                        Source = nameof(DeletarLoja),
                        StackTrace = null
                    });

                    return Ok(new { mensagem = "Loja excluída com sucesso!" });
                }
                return NotFound("Não foi possível efetuar a exclusão, loja não encontrada.");
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(DeletarLoja),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao excluir loja.");
            }
        }
    }
}
