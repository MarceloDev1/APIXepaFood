using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoServico _produtoServico;
        private readonly IEstoqueServico _estoqueServico;
        public ProdutoController(IProdutoServico produtoServico, IEstoqueServico estoqueServico)
        {
            _produtoServico = produtoServico;
            _estoqueServico = estoqueServico;
        }

        [Authorize]
        [HttpPost]
        [Route("CriarProduto")]
        public IActionResult CriarProduto([FromBody] ProdutoRequest novoProduto)
        {
            var idProduto = _produtoServico.CriarProduto(novoProduto);
            Estoque estoque = new()
            {
                IdProduto = idProduto,
                IdLoja = novoProduto.IdLoja,
                Quantidade = novoProduto.Quantidade
            };
            _estoqueServico.InserirEstoque(estoque);

            return Ok(new { mensagem = "Produto criado com sucesso!", produto = novoProduto });
        }

        [Authorize]
        [HttpGet]
        [Route("RetornarProdutos")]
        public List<Produto> RetornarProdutos()
        {
            var produtos = _produtoServico.ObterTodosProdutos();
            return produtos;
        }

        [Authorize]
        [HttpGet]
        [Route("RetornarProdutosPorIdLoja")]
        public List<Produto> RetornarProdutosPorIdLoja(int idLoja)
        {
            var produtos = _produtoServico.ObterProdutoIdLoja(idLoja);
            return produtos;
        }

        [Authorize]
        [HttpGet]
        [Route("RetornarProdutosPorId")]
        public Produto RetornarProdutosPorId(int idProduto)
        {
            var produto = _produtoServico.ObterProdutoPorId(idProduto);
            return produto;
        }

        [Authorize]
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

        [Authorize]
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