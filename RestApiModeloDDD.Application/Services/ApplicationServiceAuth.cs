using RestApiModeloDDD.Application.Dtos;
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

        public async Task<TokenDto> Login(
            LoginDto dto)
        {
            var usuario =
                await _serviceAuth.ValidarUsuario(
                    dto.Email,
                    dto.Senha);

            return new TokenDto
            {
                AccessToken =
                    _jwtService.GenerateAccessToken(usuario),

                RefreshToken =
                    _jwtService.GenerateRefreshToken(),

                Expiration =
                    DateTime.UtcNow.AddMinutes(15)
            };
        }

        public Task<TokenDto> RefreshToken(RefreshTokenDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
