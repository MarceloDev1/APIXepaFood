using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IEstoqueServico
    {
        void InserirEstoque(Estoque estoque);
        List<Estoque> ObterProdutoPorIdLoja(int idLoja);
        List<Estoque> ObterProdutoPorIdProduto(int idProduto);
        Estoque ObterEstoquePorIdLojaEIdProduto(int idProduto, int idLoja);
        void AtualizarEstoquePorId(Estoque estoque);
        Estoque ObterEstoquePorIdProduto(int idProduto);
        void DeletarEstoquePorIdProduto(int idProduto);
    }
}