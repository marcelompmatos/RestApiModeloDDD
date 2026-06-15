using RestApiModeloDDD.Application.Dtos;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServiceAuth
    {
        Task<TokenDto> Login(LoginDto dto);

        Task<TokenDto> RefreshToken(RefreshTokenDto dto);
    }
}
