using Domain.Interfaces;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : Controller
    {
        private readonly ILojaServico _lojaServico;
        public LojaController(ILojaServico lojaServico)
        {
            _lojaServico = lojaServico;
        }

        [HttpPost]
        [Route("CriarLoja")]
        public IActionResult CriarLoja([FromBody] Loja novaLoja)
        {
            if (novaLoja == null)
                return BadRequest("Dados inválidos para o cadastro.");

            _lojaServico.CriarLoja(novaLoja);
            return Ok(new { mensagem = "Loja criada com sucesso!" });
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

        [HttpPost]
        [Route("AtualizarLoja")]
        public IActionResult AtualizarLoja([FromBody] Loja loja)
        {
            var lojaExistente = _lojaServico.ObterLojaPorId(loja.IdLoja);
            if (lojaExistente == null)
                return NotFound("Loja não encontrada! ");

            _lojaServico.AtualizarLoja(lojaExistente);
            return Ok(new { mensagem = "Loja atualizada com sucesso!" });
        }

        [HttpPost]
        [Route("ExcluirLoja")]
        public IActionResult DeletarLoja(int idLoja)
        {
            var loja = _lojaServico.ObterLojaPorId(idLoja);

            if (loja != null)
            {
                _lojaServico.DeletarLoja(idLoja);
                return Ok(new { mensagem = "Loja excluída com sucesso!" });
            }
            return NotFound("Não foi possível efetuar a exclusão, loja não encontrada.");
        }
    }
}
