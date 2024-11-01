using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Loja
    {
        public int IdLoja { get; set; }
        [StringLength(100)]
        public string NomeLoja { get; set; }
        [StringLength(255)]
        public string Localizacao { get; set; }
        public int IdUsuario { get; set; }
    }
}