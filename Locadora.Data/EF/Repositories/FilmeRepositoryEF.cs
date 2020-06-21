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
    public class FilmeRepositoryEF : RepositoryEF<Filme>, IFilmeRepository
    {
        public FilmeRepositoryEF(LocadoraDataContext ctx) : base(ctx)
        { }
        public IEnumerable<Filme> GetFilmeTitulo(string titulo)
        {
            return _ctx.Filmes.Where(x => x.Titulo.Contains(titulo, StringComparison.InvariantCultureIgnoreCase) || x.TituloOriginal.Contains(titulo, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public async Task<IEnumerable<Filme>> GetFilmeTituloAsync(string titulo)
        {
            return await _ctx.Filmes.Where(x => x.Titulo.Contains(titulo, StringComparison.InvariantCultureIgnoreCase) || x.TituloOriginal.Contains(titulo, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }
    }
}
