using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(Usuario usuario);

        string GenerateRefreshToken();
    }
}
