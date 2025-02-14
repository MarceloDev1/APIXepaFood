using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Requests
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; }
        public string Localizacao { get; set; }
        public string Telefone { get; set; }
        public bool Feirante { get; set; }

        public string ValidarCamposObrigatorios()
        {
            if (string.IsNullOrWhiteSpace(Email) || Email == null)
                return "Email é obrigatório.";

            if (string.IsNullOrWhiteSpace(Senha) || Senha == null)
                return "Senha é obrigatória.";

            return null; 
        }

        public string ValidarEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Email inválido.";

            return null;
        }

        public string ValidarSenha(string senha)
        {
            if (senha.Length < 6)
                return "A senha deve ter no mínimo 6 caracteres.";

            return null;
        }
    }
}