using Locadora.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Domain.Contracts.Repositories
{
    public interface ILocacoesRepository : IRepository<Locacoes>
    {
        IEnumerable<Locacoes> GetLocacao(int idCliente, int idFilme, string dataLocacao);
        Task<IEnumerable<Locacoes>> GetLocacaoAsync(int idCliente, int idFilme, string dataLocacao);
        
        IEnumerable<Locacoes> GetLocacaoCliente(int idCliente);
        Task<IEnumerable<Locacoes>> GetLocacaoClienteAsync(int idCliente);

        IEnumerable<Locacoes> GetLocacaoFilme(int idFilme);
        Task<IEnumerable<Locacoes>> GetLocacaoFilmeAsync(int idFilme);

    }
}
