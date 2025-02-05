namespace Domain.Requests
{
    public class LojaEUsuarioRequest
    {
        public string NomeLoja { get; set; } 
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Senha { get; set; }
        public string LocalizacaoUsuario { get; set; }
        public string LocalizacaoLoja { get; set; }
        public string Telefone { get; set; }
        public bool Feirante { get; set; }
    }
}