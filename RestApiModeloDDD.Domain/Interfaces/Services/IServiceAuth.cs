using RestApiModeloDDD.Domain.Entities;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IServiceAuth
    {
        Task<Usuario> ValidarUsuario( string email, string senha);

        Task<Usuario> ValidarRefreshToken( string token);
        Task<string> GerarRefreshToken(Usuario usuario);

        Task<Usuario> RenovarRefreshToken( string refreshToken);


    }
}
