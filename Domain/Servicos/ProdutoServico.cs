using Domain.Entidades;
using Domain.Interfaces;

namespace Domain.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoServico(IProdutoRepositorio ProdutoRepositorio)
        {
            _produtoRepositorio = ProdutoRepositorio;
        }
        public void CriarProduto(Produto produto)
        {
            _produtoRepositorio.CriarProduto(produto);
        }

        public Produto ObterProdutoPorNome(string nome)
        {
            Produto produto = _produtoRepositorio.ObterProdutoPorNome(nome);
            return produto;
        }
        public List<Produto> ObterTodosProdutos()
        {
            List<Produto> listaProdutos = _produtoRepositorio.ObterTodosProdutos();
            return listaProdutos;
        }

        public Produto ObterProdutoPorId(int idProduto)
        {
            Produto produto = _produtoRepositorio.ObterProdutoPorId(idProduto);
            return produto;
        }

        public void AtualizarProdutoPorId(Produto novoProduto)
        {
            _produtoRepositorio.AtualizarProdutoPorId(novoProduto);
        }

        public void DeletarProdutoPorId(int idProduto)
        {
            _produtoRepositorio.DeletarProdutoPorId(idProduto);
        }
    }
}