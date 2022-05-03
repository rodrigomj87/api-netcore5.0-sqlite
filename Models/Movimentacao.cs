using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testedev.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ConteinerId { get; set; }
        public int TipoMovimentacaoId { get; set; }
        public Clientes Cliente { get; set; }
        public Conteiner Conteiner { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
    }
}
