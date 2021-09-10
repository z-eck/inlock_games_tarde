using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class UsuarioDomain
    {
        public int idUsuario { get; set; }
        public int idTipoUsuario { get; set; }
        [Required(ErrorMessage = "email necessário")]
        public string email { get; set; }
        [Required(ErrorMessage = "senha necessária")]
        [StringLength(16, MinimumLength = 5)]
        public string senha { get; set; }
        public TipoUsuarioDomain tipoUsuario { get; set; }
    }
}