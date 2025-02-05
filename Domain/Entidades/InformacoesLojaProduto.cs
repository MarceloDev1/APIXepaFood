namespace Domain.Entidades
{
    public class InformacoesLojaProduto
    {
        public int Quantidade { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string NomeLoja { get; set; }
        public string LocalizacaoLoja { get; set; }
        public decimal Preco { get; set; }
    }
}