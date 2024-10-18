using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IEstoqueRepositorio
    {
        void InserirEstoque(Estoque estoque);
        List<Estoque> ObterProdutoPorIdLoja(int idLoja);
        List<Estoque> ObterProdutoPorIdProduto(int idProduto);
        Estoque ObterEstoquePorIdLojaEIdProduto(int idProduto, int idLoja);
        Estoque ObterEstoqueIdProduto(int idProduto);
        void AtualizarEstoquePorId(Estoque estoque);
        void DeletarEstoquePorId(int idProduto);
    }
}