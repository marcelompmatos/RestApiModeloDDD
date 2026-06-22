using RestApiModeloDDD.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IServiceRefreshToken
    {
        Task<RefreshToken> GerarToken(Guid usuarioId);

        Task<RefreshToken> ValidarToken(string token);

        Task RevogarToken(Guid id);

        Task RevogarTokensUsuario(Guid usuarioId);

        Task AtualizarToken(RefreshToken refreshToken);
    }
}
