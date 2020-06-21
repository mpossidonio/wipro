using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
