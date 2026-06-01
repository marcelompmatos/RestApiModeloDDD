using Microsoft.EntityFrameworkCore;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly SqlContext sqlContext;

    public RepositoryBase(SqlContext sqlContext)
    {
        this.sqlContext = sqlContext;
    }

    public async Task AddAsync(TEntity obj)
    {
        await sqlContext.Set<TEntity>().AddAsync(obj);
        await sqlContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await sqlContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await sqlContext.Set<TEntity>().FindAsync(id);
    }

    public async Task RemoveAsync(TEntity obj)
    {
        sqlContext.Set<TEntity>().Remove(obj);
        await sqlContext.SaveChangesAsync();

    } 

    public async Task UpdateAsync(TEntity obj)
    {
        var id = (int)typeof(TEntity)
        .GetProperty("Id")
        .GetValue(obj);

        var entidadeExistente = await GetByIdAsync(id);

        sqlContext.Entry(entidadeExistente)
                  .CurrentValues
                  .SetValues(obj);

        await sqlContext.SaveChangesAsync();


    }
}