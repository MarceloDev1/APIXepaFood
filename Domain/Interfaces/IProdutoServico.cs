using Domain.Entidades;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IProdutoServico
    {
        void CriarProduto(ProdutoRequest produto);
        Produto ObterProdutoPorNome(string nome);
        List<Produto> ObterTodosProdutos();
        Produto ObterProdutoPorId(int idProduto);
        void AtualizarProdutoPorId(Produto novoProduto);
        void DeletarProdutoPorId(int idProduto);
    }
}