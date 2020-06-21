using Locadora.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Locadora.Domain.Contracts.Repositories
{

    public interface IRepository<T> where T : Entity
    {

        void Add(T entity);
        void Edit(T entity);

        T Get(int id);
        Task<T> GetAsync(int id);

        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();

    }
}
