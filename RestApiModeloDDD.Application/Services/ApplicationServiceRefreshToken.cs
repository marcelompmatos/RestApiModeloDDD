using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.DTOs;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceRefreshToken : IApplicationServiceRefreshToken
    {
        private readonly IServiceRefreshToken _serviceRefreshToken;

        public ApplicationServiceRefreshToken(
            IServiceRefreshToken serviceRefreshToken)
        {
            _serviceRefreshToken = serviceRefreshToken;
        }

        public async Task<RefreshTokenDto> GerarToken(int usuarioId)
        {
            var refreshToken =
                await _serviceRefreshToken.GerarToken(usuarioId);

            return new RefreshTokenDto
            {
                RefreshToken = refreshToken.TokenHash
            };
        }

        public async Task<bool> ValidarToken(string token)
        {
            var refreshToken =
                await _serviceRefreshToken.ValidarToken(token);

            return refreshToken != null;
        }

        public async Task RevogarToken(string token)
        {
            var refreshToken =
                await _serviceRefreshToken.ValidarToken(token);

            if (refreshToken == null)
                throw new UnauthorizedAccessException(
                    "Refresh Token inválido.");

            await _serviceRefreshToken.RevogarToken(
                refreshToken.Id);
        }

        public Task<TokenDto> RefreshToken(RefreshTokenDto dto)
        {
            throw new NotImplementedException();
        }
    }
}