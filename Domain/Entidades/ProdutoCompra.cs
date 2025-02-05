namespace Domain.Entidades
{
    public class ProdutoCompra
    {
            public int IdProduto { get; set; } // ID do produto
            public int Quantidade { get; set; } // Quantidade do produto
            public decimal PrecoUnitario { get; set; } // Preço unitário do produto na compra
    }
}