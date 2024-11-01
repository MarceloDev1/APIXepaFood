using Domain.Entidades;
using Domain.Interfaces;

namespace Domain.Servicos
{
    public class EstoqueServico : IEstoqueServico
    {
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        public EstoqueServico(IEstoqueRepositorio estoqueRepositorio)
        {
            _estoqueRepositorio = estoqueRepositorio;
        }


        public void InserirEstoque(Estoque estoque)
        {
            _estoqueRepositorio.InserirEstoque(estoque);
        }


        public List<Estoque> ObterProdutoPorIdLoja(int idLoja)
        {
            var listaEstoque = _estoqueRepositorio.ObterProdutoPorIdLoja(idLoja);
            return listaEstoque;
        }

        public List<Estoque> ObterProdutoPorIdProduto(int idProduto)
        {
            var listaEstoque = _estoqueRepositorio.ObterProdutoPorIdProduto(idProduto);
            return listaEstoque;
        }
        public Estoque ObterEstoquePorIdLojaEIdProduto(int idProduto, int idLoja)
        {
            var estoque = _estoqueRepositorio.ObterEstoquePorIdLojaEIdProduto(idProduto, idLoja);
            return estoque;
        }
        public void AtualizarEstoquePorId(Estoque estoque)
        {
            _estoqueRepositorio.AtualizarEstoquePorId(estoque);
        }

        public Estoque ObterEstoquePorIdProduto(int idProduto)
        {
            Estoque estoque = _estoqueRepositorio.ObterEstoqueIdProduto(idProduto);
            return estoque;
        }

        public void DeletarEstoquePorIdProduto(int idProduto)
        {
            _estoqueRepositorio.DeletarEstoquePorIdProduto(idProduto);
        }

        public List<InformacoesLojaProduto> ObterEstoque(string nomeProduto, string localizacao)
        {
           var lista = new List<InformacoesLojaProduto>();
            lista = _estoqueRepositorio.ObterEstoque(nomeProduto, localizacao);
            return lista;
        }
    }
}