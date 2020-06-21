using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class FilmeVM
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string TituloOriginal { get; set; }
        public bool NoCatalogo { get; set; }
    }
}
