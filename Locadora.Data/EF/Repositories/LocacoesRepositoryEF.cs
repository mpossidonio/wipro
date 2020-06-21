using Locadora.Domain.Contracts.Repositories;
using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Data.EF.Repositories
{
    public class LocacoesRepositoryEF : RepositoryEF<Locacoes>, ILocacoesRepository
    {
        public LocacoesRepositoryEF(LocadoraDataContext ctx) : base(ctx)
        { }

        public IEnumerable<Locacoes> GetLocacao(int idCliente, int idFilme, string dataLocacao)
        {
            return _ctx.Locacoes.Where(x => x.IdCliente == idCliente && x.IdFilme == idFilme && DateTime.Compare(x.DataLocacao, Convert.ToDateTime(dataLocacao))==0).ToList();
        }

        public async Task<IEnumerable<Locacoes>> GetLocacaoAsync(int idCliente, int idFilme, string dataLocacao)
        {
            return await _ctx.Locacoes.Where(x => x.IdCliente == idCliente && x.IdFilme == idFilme && DateTime.Compare(x.DataLocacao, Convert.ToDateTime(dataLocacao))==0).ToListAsync();
        }

        public IEnumerable<Locacoes> GetLocacaoCliente(int idCliente)
        {
            return _ctx.Locacoes.Where(x => x.IdCliente==idCliente).ToList();
        }

        public async Task<IEnumerable<Locacoes>> GetLocacaoClienteAsync(int idCliente)
        {
            return await _ctx.Locacoes.Where(x => x.IdCliente==idCliente).ToListAsync();
        }

        public IEnumerable<Locacoes> GetLocacaoFilme(int idFilme)
        {
            return _ctx.Locacoes.Where(x => x.IdFilme == idFilme).ToList();
        }

        public async Task<IEnumerable<Locacoes>> GetLocacaoFilmeAsync(int idFilme)
        {
            return await _ctx.Locacoes.Where(x => x.IdFilme == idFilme).ToListAsync();
        }
    }
}
