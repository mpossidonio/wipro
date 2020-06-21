using Locadora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.Contracts.Repositories
{
    public interface IFilmeRepository : IRepository<Filme>
    {
        IEnumerable<Filme> GetFilmeTitulo(string titulo);
        Task<IEnumerable<Filme>> GetFilmeTituloAsync(string titulo);

    }
}
