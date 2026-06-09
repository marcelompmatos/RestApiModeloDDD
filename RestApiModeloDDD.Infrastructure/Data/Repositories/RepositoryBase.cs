using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
{
    private readonly SqlContext _sqlContext;
    private readonly ILogger<RepositoryBase<TEntity>> _logger;

    public RepositoryBase(
        SqlContext sqlContext,
        ILogger<RepositoryBase<TEntity>> logger)
    {
        _sqlContext = sqlContext;
        _logger = logger;
    }

    public async Task AddAsync(TEntity obj)
    {
        try
        {
            await _sqlContext.Set<TEntity>().AddAsync(obj);
            await _sqlContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Erro ao inserir entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Erro inesperado ao inserir entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _sqlContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _sqlContext.Set<TEntity>().FindAsync(id);
    }

    public async Task RemoveAsync(TEntity obj)
    {
        try
        {
            _sqlContext.Set<TEntity>().Remove(obj);
            await _sqlContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Erro ao remover entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Erro inesperado ao remover entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
    }

    public async Task UpdateAsync(TEntity obj)
    {
        try
        {
            var propertyId = typeof(TEntity).GetProperty("Id");

            if (propertyId == null)
                throw new InvalidOperationException(
                    $"A entidade {typeof(TEntity).Name} não possui propriedade Id.");

            var id = (int)propertyId.GetValue(obj)!;

            var entidadeExistente = await GetByIdAsync(id);

            if (entidadeExistente == null)
                throw new KeyNotFoundException(
                    $"{typeof(TEntity).Name} com Id {id} não encontrada.");

            _sqlContext.Entry(entidadeExistente)
                .CurrentValues
                .SetValues(obj);

            await _sqlContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Erro ao atualizar entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Erro inesperado ao atualizar entidade {Entity}.",
                typeof(TEntity).Name);

            throw;
        }
    }
}