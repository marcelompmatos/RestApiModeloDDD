using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.DTOs;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceAuth  : IApplicationServiceAuth
    {
        private readonly IServiceAuth _serviceAuth;
        private readonly IJwtService _jwtService;

        public ApplicationServiceAuth(
            IServiceAuth serviceAuth,
            IJwtService jwtService)
        {
            _serviceAuth = serviceAuth;
            _jwtService = jwtService;
        }

        public async Task<TokenDto> Login(LoginDto dto)
        {
            var usuario =
                await _serviceAuth.ValidarUsuario(
                    dto.Email,
                    dto.Senha);

            var accessToken =
                _jwtService.GenerateAccessToken(usuario);

            var refreshToken =
                await _serviceAuth
                    .GerarRefreshToken(usuario);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<TokenDto> RefreshToken(RefreshTokenDto dto)
        {
            var usuario =
                await _serviceAuth
                    .RenovarRefreshToken(dto.RefreshToken);

            var accessToken =
                _jwtService.GenerateAccessToken(usuario);

            var novoRefreshToken =
                await _serviceAuth
                    .GerarRefreshToken(usuario);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = novoRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
