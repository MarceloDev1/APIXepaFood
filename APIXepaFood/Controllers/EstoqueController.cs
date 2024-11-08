using Domain.Entidades;
using Domain.Interfaces;
using Domain.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueServico _estoqueServico;
        public EstoqueController(IEstoqueServico estoqueServico)
        {
            _estoqueServico = estoqueServico;
        }

        [HttpPost]
        [Route("InserirEstoque")]
        public IActionResult InserirEstoque([FromBody] Estoque estoque)
        {
            if (estoque == null)
                return BadRequest("Dados inválidos.");

            _estoqueServico.InserirEstoque(estoque);
            return Ok(new { mensagem = "Estoque inserido com sucesso!", usuario = estoque });
        }

        [HttpGet]
        [Route("RetornarEstoquePorIdProduto/{idProduto}")]
        public List<Estoque> RetornarEstoquePorIdProduto(int idProduto)
        {
            var listaEstoque = _estoqueServico.ObterProdutoPorIdProduto(idProduto);
            return listaEstoque;
        }

        [HttpGet]
        [Route("RetornarEstoquePorIdLoja/{idLoja}")]
        public List<Estoque> RetornarEstoquePorIdLoja(int idLoja)
        {
            var listaUsuarios = _estoqueServico.ObterProdutoPorIdLoja(idLoja);
            return listaUsuarios;
        }

        [HttpGet]
        [Route("RetornarEstoque")]
        public List<Loja> RetornarEstoque([FromQuery] string nomeProduto = null, string localizacao = null)
        {
            var listaUsuarios = _estoqueServico.ObterEstoque(nomeProduto, localizacao);
            return listaUsuarios;
        }

        [HttpPut]
        [Route("AtualizarEstoquePorId")]
        public IActionResult AtualizarUsuarioPorId([FromBody] Estoque estoque)
        {
            var estoqueExistente = _estoqueServico.ObterEstoquePorIdLojaEIdProduto(estoque.IdProduto, estoque.IdLoja);

            if (estoqueExistente == null)
            {
                return NotFound("Estoque não encontrado.");
            }

            _estoqueServico.AtualizarEstoquePorId(estoque);

            return Ok("Estoque atualizado com sucesso.");
        }

        [HttpDelete]
        [Route("DeletarEstoquePorId/{idProduto}")]
        public IActionResult DeletarEstoquePorId(int idProduto)
        {
            var estoqueExistente = _estoqueServico.ObterEstoquePorIdProduto(idProduto);

            if (estoqueExistente == null)
            {
                return NotFound("Estoque não encontrado.");
            }

            _estoqueServico.DeletarEstoquePorIdProduto(idProduto);

            return Ok("Estoque deletado com sucesso.");
        }
    }
}