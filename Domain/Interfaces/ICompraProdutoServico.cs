using APIXepaFood.Controllers;

namespace Domain.Interfaces
{
    public interface ICompraProdutoServico
    {
        void ComprarProduto(CompraProdutoRequest compraProdutoServico);
    }
}