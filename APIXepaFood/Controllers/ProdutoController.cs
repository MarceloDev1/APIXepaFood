using Domain.Entidades;
using Domain.Interfaces;
using Domain.Servicos;
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

        [HttpGet]
        [Route("RetornarProdutos")]
        public List<Produto> RetornarProdutos()
        {
            var produtos = _produtoServico.ObterTodosProdutos();
            return produtos;
        }

        [HttpGet]
        [Route("RetornarProdutosPorId")]
        public Produto RetornarProdutosPorId(int idProduto)
        {
            var produto = _produtoServico.ObterProdutoPorId(idProduto);
            return produto;
        }

        [HttpPut]
        [Route("AtualizarProdutoPorId")]
        public IActionResult AtualizarProdutoPorId([FromBody] Produto novoProduto)
        {
            var produtoExistente = _produtoServico.ObterProdutoPorId(novoProduto.IdProduto);

            if (produtoExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _produtoServico.AtualizarProdutoPorId(novoProduto);

            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete]
        [Route("DeletarProdutoPorId/{idProduto}")]
        public IActionResult DeletarProdutoPorId(int idProduto)
        {
            var produtoExistente = _produtoServico.ObterProdutoPorId(idProduto);

            if (produtoExistente == null)
            {
                return NotFound("Produto não encontrado.");
            }

            _produtoServico.DeletarProdutoPorId(idProduto);

            return Ok("Produto deletado com sucesso.");
        }
    }
}