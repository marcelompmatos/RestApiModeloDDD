using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Core.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);

        Task UpdateAsync(TEntity obj);

        Task RemoveAsync(TEntity obj);

        Task<IEnumerable<TEntity>> GetAllAsync();   

        Task<TEntity> GetByIdAsync(int id);
    }
}