using Locadora.Domain.Contracts.Repositories;
using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Data.EF.Repositories
{
    public class ClienteRepositoryEF : RepositoryEF<Cliente>, IClienteRepository
    {
        public ClienteRepositoryEF(LocadoraDataContext ctx) : base(ctx)
        { }
        public IEnumerable<Cliente> GetClienteCPF(string cpf)
        {
            return _ctx.Clientes.Where(x => x.CPF.Equals(cpf)).ToList();
        }

        public async Task<IEnumerable<Cliente>> GetClienteCPFAsync(string cpf)
        {
            return await _ctx.Clientes.Where(x => x.CPF.Equals(cpf)).ToListAsync();
        }

        public IEnumerable<Cliente> GetClienteNome(string nome)
        {
            return _ctx.Clientes.Where(x => x.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public async Task<IEnumerable<Cliente>> GetClienteNomeAsync(string nome)
        {
            return await _ctx.Clientes.Where(x => x.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

    }
}
