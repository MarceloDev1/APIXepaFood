using APIXepaFood.Controllers;

namespace Domain.Interfaces
{
    public interface ICompraProdutoRepositorio
    {
        void AtualizarEstoque(int idProduto, int quantidade);
        int ComprarProduto(int idUsuario, DateTime dataCompra, decimal valorTotalCompra, int idStatusCompra);
        void RegistrarItemCompra(int idCompra, int idProduto, int quantidade, decimal precoUnitario);
        int RetornarQtdProdutoEstoque(int idProduto);
    }
}