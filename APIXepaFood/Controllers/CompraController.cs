using Domain.Interfaces;
using Domain.Requests;
using Domain.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        readonly ICompraProdutoServico _compraProdutoServico;
        public CompraController(ICompraProdutoServico compraProdutoServico)
        {
            _compraProdutoServico = compraProdutoServico;
        }

        [HttpPost]
        [Route("ComprarProduto")]
        public IActionResult ComprarProduto([FromBody] CompraProdutoRequest compra)
        {
            if (compra == null)
                return BadRequest("Dados inválidos.");
            _compraProdutoServico.ComprarProduto(compra);
            
            return Ok(new { mensagem = "Compra efetuada com sucesso!", compra = compra });
        }
    }
}