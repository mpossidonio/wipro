using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Domain.Entities
{
    public class Locacoes : Entity
    {
        public int IdCliente { get; set; }
        public int IdFilme { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DevolucaoPrevista { get; set; }
        public DateTime? Devolucao { get; set; }
    }
}
