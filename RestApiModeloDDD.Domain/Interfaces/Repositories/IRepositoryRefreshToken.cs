using RestApiModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Repositories
{
    public interface IRepositoryRefreshToken
    {
        Task Adicionar(RefreshToken refreshToken);

        Task<RefreshToken> ObterPorToken(string token);

        Task<IEnumerable<RefreshToken>> ObterPorUsuario(int usuarioId);

        Task Atualizar(RefreshToken refreshToken);

        Task Remover(int id);
    }
}
