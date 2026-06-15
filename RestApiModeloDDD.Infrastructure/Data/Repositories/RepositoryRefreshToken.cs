using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryRefreshToken :
        RepositoryBase<RefreshToken>,
        IRepositoryRefreshToken
    {
        private readonly SqlContext _context;
        private readonly ILogger<RepositoryRefreshToken> _logger;

        public RepositoryRefreshToken(
            SqlContext context,
            ILogger<RepositoryBase<RefreshToken>> logger,
            ILogger<RepositoryRefreshToken> refreshTokenLogger)
            : base(context, logger)
        {
            _context = context;
            _logger = refreshTokenLogger;
        }

        public async Task Adicionar(RefreshToken refreshToken)
        {
            try
            {
                _logger.LogInformation(
                    "Adicionando RefreshToken para usuário {UsuarioId}",
                    refreshToken.UsuarioId);

                await AddAsync(refreshToken);

                _logger.LogInformation(
                    "RefreshToken criado. Id: {Id}",
                    refreshToken.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao adicionar RefreshToken.");

                throw;
            }
        }

        public async Task<RefreshToken?> ObterPorToken(string token)
        {
            try
            {
                _logger.LogInformation(
                    "Consultando RefreshToken.");

                var refreshToken =
                    await _context.RefreshTokens
                        .AsNoTracking()
                        .Include(x => x.Usuario)
                        .FirstOrDefaultAsync(x => x.TokenHash == token);

                if (refreshToken == null)
                {
                    _logger.LogWarning(
                        "RefreshToken não encontrado.");

                    return null;
                }

                return refreshToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao consultar RefreshToken.");

                throw;
            }
        }

        public async Task<IEnumerable<RefreshToken>> ObterPorUsuario(Guid usuarioId)
        {
            try
            {
                return await _context.RefreshTokens
                    .AsNoTracking()
                    .Where(x => x.UsuarioId == usuarioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao consultar tokens do usuário {UsuarioId}",
                    usuarioId);

                throw;
            }
        }

        public async Task Atualizar(RefreshToken refreshToken)
        {
            try
            {
                await UpdateAsync(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao atualizar RefreshToken {Id}",
                    refreshToken.Id);

                throw;
            }
        }

        public async Task Remover(Guid id)
        {
            try
            {
                var refreshToken =
                    await _context.RefreshTokens
                        .FirstOrDefaultAsync(x => x.Id == id);

                if (refreshToken == null)
                    return;

                await RemoveAsync(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao remover RefreshToken {Id}",
                    id);

                throw;
            }
        }
    }
}