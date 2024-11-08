using Domain.Entidades;

namespace APIXepaFood.Controllers
{
    public class CompraProdutoRequest
    {
        public CompraProdutoRequest()
        {
            Produtos = new List<ProdutoCompra>();
        }
        public int IdUsuario { get; set; } // ID do usuário que está realizando a compra
        public int IdStatusCompra { get; set; } // ID do status da compra
        public List<ProdutoCompra> Produtos { get; set; }// Lista de produtos na compra
    }
}