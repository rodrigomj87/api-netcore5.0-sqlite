using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testedev.Models.ViewModel
{
    public class ConteinerVM
    {
        [Required(ErrorMessage = "O Número do conteiner deve ser informado!")]
        [Display(Name = "Número do contêiner")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Número do Contêiner de ter 11 caracteres")]
        [RegularExpression(@"^([A-Z]{4})([0-9]{7})$", ErrorMessage = "O formato de ver ser 4 letras seguidas de 7 números(TEST1234567)")]
        public string ConteinerNumero { get; set; }

        [Required(ErrorMessage = "O Tipo do Contêiner deve ser informado!")]
        [Display(Name = "Tipo")]
        [RegularExpression(@"^([2][0]|[4][0])$", ErrorMessage = "O Tipo do contêiner é invalido (20 ou 40)")]
        public byte ConteinerTipo { get; set; }

        [Required(ErrorMessage = "O Status do conteiner deve ser informado!")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "A Categoria do conteiner deve ser informada!")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}
