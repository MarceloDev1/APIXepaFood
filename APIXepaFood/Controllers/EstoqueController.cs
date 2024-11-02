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
        private readonly LogMongoService _logMongoService;
        public EstoqueController(IEstoqueServico estoqueServico, LogMongoService logMongoService)
        {
            _estoqueServico = estoqueServico;
            _logMongoService = logMongoService;
        }

        [HttpPost]
        [Route("InserirEstoque")]
        public IActionResult InserirEstoque([FromBody] Estoque estoque)
        {
            try
            {
                if (estoque == null)
                    return BadRequest("Dados inválidos.");

                _estoqueServico.InserirEstoque(estoque);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Estoque inserido: Loja: {estoque.IdLoja}, Produto: {estoque.IdProduto}, Quatidade: {estoque.Quantidade}",
                    Source = nameof(InserirEstoque),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Estoque inserido com sucesso!", usuario = estoque });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = ex.Message,
                    Source = nameof(InserirEstoque),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao adicionar estoque");
            }
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

        [HttpPut]
        [Route("AtualizarEstoquePorId")]
        public IActionResult AtualizarUsuarioPorId([FromBody] Estoque estoque)
        {
            try
            {

                var estoqueExistente = _estoqueServico.ObterEstoquePorIdLojaEIdProduto(estoque.IdProduto, estoque.IdLoja);

                if (estoqueExistente == null)
                {
                    return NotFound("Estoque não encontrado.");
                }

                _estoqueServico.AtualizarEstoquePorId(estoque);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Estoque Atualizado: Loja: {estoque.IdLoja}, Produto: {estoque.IdProduto}, Quatidade: {estoque.Quantidade}",
                    Source = nameof(AtualizarUsuarioPorId),
                    StackTrace = null
                });

                return Ok("Estoque atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = ex.Message,
                    Source = nameof(AtualizarUsuarioPorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao atualizar estoque");
            }
        }

        [HttpDelete]
        [Route("DeletarEstoquePorId/{idProduto}")]
        public IActionResult DeletarEstoquePorId(int idProduto)
        {
            try
            {
                var estoqueExistente = _estoqueServico.ObterEstoquePorIdProduto(idProduto);

                if (estoqueExistente == null)
                {
                    return NotFound("Estoque não encontrado.");
                }

                _estoqueServico.DeletarEstoquePorIdProduto(idProduto);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Estoque excluído: Loja: {estoqueExistente.IdLoja}, Produto: {estoqueExistente.IdProduto}, Quatidade: {estoqueExistente.Quantidade}",
                    Source = nameof(DeletarEstoquePorId),
                    StackTrace = null
                });

                return Ok("Estoque deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = ex.Message,
                    Source = nameof(DeletarEstoquePorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao deletar estoque");
            }
        }
    }
}