using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class LojaRequest
    {
        public int IdUsuario { get; set; }
        [StringLength(100)]
        public string NomeLoja { get; set; }
        [StringLength(255)]
        public string Localizacao { get; set; }
    }
}