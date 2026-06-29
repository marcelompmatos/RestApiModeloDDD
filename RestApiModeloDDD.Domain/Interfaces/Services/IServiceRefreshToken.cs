using RestApiModeloDDD.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IServiceRefreshToken
    {
        Task<RefreshToken> GerarToken(int  usuarioId);

        Task<RefreshToken> ValidarToken(string token);

        Task RevogarToken(int id);

        Task RevogarTokensUsuario(int usuarioId);

        Task AtualizarToken(RefreshToken refreshToken);
    }
}
