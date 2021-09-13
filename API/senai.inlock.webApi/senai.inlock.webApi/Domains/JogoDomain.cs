using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domain
{
    public class JogoDomain
    {
        public int idJogo { get; set; }
        public int idEstudio { get; set; }

        [Required(ErrorMessage = "Informe o nome do Jogo!")]
        public string nomeJogo { get; set; }

        [Required(ErrorMessage = "Insira uma descrição ao Jogo!")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Insira o Valor do Jogo!")]
        public decimal valor { get; set; }

        [DataType(DataType.Date)]
        public DateTime dataLancamento { get; set; }
        EstudioDomain estudio { get; set; }
    }
}
