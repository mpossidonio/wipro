using Locadora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.Contracts.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> GetClienteCPF(string cpf);
        Task<IEnumerable<Cliente>> GetClienteCPFAsync(string cpf);

        IEnumerable<Cliente> GetClienteNome(string nome);
        Task<IEnumerable<Cliente>> GetClienteNomeAsync(string nome);
    }
}
