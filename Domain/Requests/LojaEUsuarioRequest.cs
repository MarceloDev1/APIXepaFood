namespace Domain.Requests
{
    public class LojaEUsuarioRequest
    {
        public string NomeLoja { get; set; } 
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Senha { get; set; }
        public string CEPLoja { get; set; }
        public string CEPUsuario { get; set; }
        public string LogradouroLoja { get; set; }
        public string LogradouroUsuario { get; set; }
        public string BairroLoja { get; set; }
        public string BairroUsuario { get; set; }
        public string UFLoja { get; set; }
        public string UFUsuario { get; set; }
        public string CidadeLoja { get; set; }
        public string CidadeUsuario { get; set; }
        public string Telefone { get; set; }
        public bool Feirante { get; set; }
    }
}