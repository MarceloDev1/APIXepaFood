using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IProdutoServico
    {
        void CriarProduto(Produto produto);
        Produto ObterProdutoPorNome(string nome);
        List<Produto> ObterTodosProdutos();
        Produto ObterProdutoPorId(int idProduto);
        void AtualizarProdutoPorId(Produto novoProduto);
        void DeletarProdutoPorId(int idProduto);
    }
}