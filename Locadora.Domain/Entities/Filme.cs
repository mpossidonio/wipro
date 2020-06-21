using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Domain.Entities
{
    public class Filme : Entity
    {
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string TituloOriginal { get; set; }
        public bool NoCatalogo { get; set; } = true;
    }
}
