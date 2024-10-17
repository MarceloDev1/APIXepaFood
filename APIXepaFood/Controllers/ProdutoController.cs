using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoServico _produtoServico;
        public ProdutoController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        [HttpPost]
        [Route("CriarProduto")]
        public IActionResult CriarProduto([FromBody] Produto novoProduto)
        {
            //if (novoProduto == null)
            //    return BadRequest("Dados inválidos.");

            //var produtoExistente = _produtoRepositorio.ObterProdutoPorEmail(novoProduto.Email);
            //if (produtoExistente != null)
            //    return Conflict("Este produto já existe!");

            _produtoServico.CriarProduto(novoProduto);
            return Ok(new { mensagem = "Produto criado com sucesso!", produto = novoProduto });
        }
    }
}