using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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