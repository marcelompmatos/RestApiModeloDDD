using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Services
{
    public class ServiceRefreshToken : IServiceRefreshToken
    {
        private readonly IRepositoryRefreshToken _repository;

        public ServiceRefreshToken(
            IRepositoryRefreshToken repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GerarToken(
            Guid usuarioId)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                TokenHash = Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64)),
                ExpiraEm = DateTime.UtcNow.AddDays(7),
                Revogado = false
            };

            await _repository.Adicionar(refreshToken);

            return refreshToken;
        }

        public async Task<RefreshToken> ValidarToken(
            string token)
        {
            var refreshToken =
                await _repository.ObterPorToken(token);

            if (refreshToken == null)
                return null;

            if (refreshToken.Revogado)
                return null;

            if (refreshToken.ExpiraEm < DateTime.UtcNow)
                return null;

            return refreshToken;
        }

        public async Task RevogarToken(Guid id)
        {
            var refreshToken =
                await _repository.ObterPorToken(id.ToString());

            if (refreshToken == null)
                return;

            refreshToken.Revogado = true;

            await _repository.Atualizar(refreshToken);
        }

        public async Task RevogarTokensUsuario(
            Guid usuarioId)
        {
            var tokens =
                await _repository.ObterPorUsuario(usuarioId);

            foreach (var token in tokens)
            {
                token.Revogado = true;
                await _repository.Atualizar(token);
            }
        }

        public async Task AtualizarToken(
            RefreshToken refreshToken)
        {
            await _repository.Atualizar(refreshToken);
        }
    }
}