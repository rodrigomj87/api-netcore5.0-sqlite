using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testedev.Models
{
    public class Conteiner
    {
        public int Id{ get; set; }
        public string ConteinerNumero { get; set; }
        public byte ConteinerTipo { get; set; }
        public int StatusId { get; set; }
        public int CategoriaId { get; set; }
        public Status Status { get; set; }
        public Categoria Categoria { get; set; }
    }
}
