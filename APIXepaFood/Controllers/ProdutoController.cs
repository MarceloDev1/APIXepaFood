using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoServico _produtoServico;
        private readonly LogMongoService _logMongoService;
        public ProdutoController(IProdutoServico produtoServico, LogMongoService logMongoService)
        {
            _produtoServico = produtoServico;
            _logMongoService = logMongoService;
        }

        [HttpPost]
        [Route("CriarProduto")]
        public IActionResult CriarProduto([FromBody] ProdutoRequest novoProduto)
        {
            try
            {
                //if (novoProduto == null)
                //    return BadRequest("Dados inválidos.");

                //var produtoExistente = _produtoRepositorio.ObterProdutoPorEmail(novoProduto.Email);
                //if (produtoExistente != null)
                //    return Conflict("Este produto já existe!");

                _produtoServico.CriarProduto(novoProduto);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Produto criado com sucesso: Produto: {novoProduto.NomeProduto}, Preço: {novoProduto.Preco}, Descrição: {novoProduto.Descricao}",
                    Source = nameof(CriarProduto),
                    StackTrace = null
                });

                return Ok(new { mensagem = "Produto criado com sucesso!", produto = novoProduto });
            }
            catch (Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(CriarProduto),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao Registrar produto");
            }
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
            try
            {
                var produtoExistente = _produtoServico.ObterProdutoPorId(novoProduto.IdProduto);

                if (produtoExistente == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                _produtoServico.AtualizarProdutoPorId(novoProduto);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Produto atualizado com sucesso: Produto: {novoProduto.NomeProduto}, Preço: {novoProduto.Preco}, Descrição: {novoProduto.Descricao}",
                    Source = nameof(AtualizarProdutoPorId),
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
                    Source = nameof(AtualizarProdutoPorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao atualizar produto");
            }
        }

        [HttpDelete]
        [Route("DeletarProdutoPorId/{idProduto}")]
        public IActionResult DeletarProdutoPorId(int idProduto)
        {
            try
            {

                var produtoExistente = _produtoServico.ObterProdutoPorId(idProduto);

                if (produtoExistente == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                _produtoServico.DeletarProdutoPorId(idProduto);

                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Info",
                    Message = $"Produto deletado com sucesso: IdProduto: {produtoExistente.IdProduto}, Produto: {produtoExistente.NomeProduto}, Preço: {produtoExistente.Preco}, Descrição: {produtoExistente.Descricao}",
                    Source = nameof(DeletarProdutoPorId),
                    StackTrace = null
                });

                return Ok("Produto deletado com sucesso.");
            }
            catch(Exception ex)
            {
                _logMongoService.LogAsync(new LogMongo
                {
                    Level = "Error",
                    Message = ex.Message,
                    Source = nameof(DeletarProdutoPorId),
                    StackTrace = ex.StackTrace
                });
                return StatusCode(500, "Erro ao deletar produto");
            }
        }
    }
}