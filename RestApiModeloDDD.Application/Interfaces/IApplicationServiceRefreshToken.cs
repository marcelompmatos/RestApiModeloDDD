using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.DTOs;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServiceRefreshToken
    {
        Task<TokenDto> RefreshToken(RefreshTokenDto dto);

        Task RevogarToken(string refreshToken);
    }
}