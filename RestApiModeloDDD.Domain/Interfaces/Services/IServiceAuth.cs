using RestApiModeloDDD.Domain.Entities;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IServiceAuth
    {
        Task<Usuario> ValidarUsuario(string email, string senha);
    }
}
