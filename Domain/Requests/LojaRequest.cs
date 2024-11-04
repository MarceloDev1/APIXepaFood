using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class LojaRequest
    {
        public int IdUsuario { get; set; }
        [StringLength(100)]
        public string NomeLoja { get; set; }
        [StringLength(255)]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
    }
}