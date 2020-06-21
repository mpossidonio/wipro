using Locadora.Domain.Contracts.Repositories;
using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Data.EF.Repositories
{

    public class RepositoryEF<T> : IRepository<T>
        where T : Entity
    {
        protected readonly LocadoraDataContext _ctx;

        public RepositoryEF(LocadoraDataContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(T entity)
        {
            _ctx.Set<T>().Add(entity);
            Save();
        }

        public void Edit(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

        private void Save()
        {
            _ctx.SaveChanges();
        }

        public T Get(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public IEnumerable<T> Get()
        {
            return _ctx.Set<T>().ToList();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }
    }
}
