using RestApiModeloDDD.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario
    {
        Task<Usuario> ObterPorEmail(string email);

        Task<Usuario> ObterPorId(Guid id);

        Task Adicionar(Usuario usuario);

        Task Atualizar(Usuario usuario);
    }
}
