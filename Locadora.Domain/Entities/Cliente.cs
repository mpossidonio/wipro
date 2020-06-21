using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Domain.Entities
{
    public class Cliente : Entity
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
    }
}
