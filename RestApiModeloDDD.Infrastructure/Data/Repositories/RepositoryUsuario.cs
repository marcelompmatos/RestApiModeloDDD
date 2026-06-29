using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryUsuario :
        RepositoryBase<Usuario>,
        IRepositoryUsuario
    {
        private readonly SqlContext _context;
        private readonly ILogger<RepositoryUsuario> _logger;

        public RepositoryUsuario(
            SqlContext context,
            ILogger<RepositoryBase<Usuario>> logger,
            ILogger<RepositoryUsuario> usuarioLogger)
            : base(context, logger)
        {
            _context = context;
            _logger = usuarioLogger;
        }

        public async Task<Usuario> ObterPorEmail(string email)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario> ObterPorId(int id)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Usuario usuario)
        {
            await AddAsync(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            try
            {
                await UpdateAsync(usuario);

                _logger.LogInformation(
                    "Usuário atualizado com sucesso. Id: {Id}",
                    usuario.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao atualizar usuário. Id: {Id}",
                    usuario.Id);

                throw;
            }
        }
    }
}