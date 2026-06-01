using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(TEntity obj)
        {
            await repository.AddAsync(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException($"Cliente {id} não encontrado.");

            await repository.RemoveAsync(entity);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await repository.UpdateAsync(obj);
        }
    }
}