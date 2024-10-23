namespace Domain.Requests
{
    public class ProdutoRequest
    {
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
    }
}