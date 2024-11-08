using APIXepaFood.Controllers;
using Domain.Interfaces;

namespace Domain.Servicos
{
    public class CompraProdutoServico : ICompraProdutoServico
    {
        readonly ICompraProdutoRepositorio _compraProdutoRepositorio;
        public CompraProdutoServico(ICompraProdutoRepositorio compraProdutoRepositorio)
        {
            _compraProdutoRepositorio = compraProdutoRepositorio;
        }
        public void ComprarProduto(CompraProdutoRequest compraProdutoServico)
        {
            var idUsuario = compraProdutoServico.IdUsuario;
            var dataCompra = DateTime.Now;
            var valorTotal = compraProdutoServico.Produtos.Sum(x => x.PrecoUnitario * x.Quantidade);
            var idStatusCompra = 3;

            var idCompra = _compraProdutoRepositorio.ComprarProduto(idUsuario, dataCompra, valorTotal, idStatusCompra);

            foreach (var item in compraProdutoServico.Produtos)
            {

                _compraProdutoRepositorio.RegistrarItemCompra(idCompra, item.IdProduto, item.Quantidade, item.PrecoUnitario);
                _compraProdutoRepositorio.AtualizarEstoque(item.IdProduto, item.Quantidade);
            }
        }
    }
}