using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class LocacoesVM
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public int IdFilme { get; set; }
        public string Titulo { get; set; }
        public string TituloOriginal { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DevolucaoPrevista { get; set; }
        public DateTime? Devolucao { get; set; }
        public string Alerta { get; set; }
    }
}
