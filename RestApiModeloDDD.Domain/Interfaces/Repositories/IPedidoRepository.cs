using RestApiModeloDDD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task RemoveAsync(Pedido pedido);

        Task<Pedido> GetByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetAllAsync();

        Task<IEnumerable<Pedido>> GetByClienteAsync(int clienteId);

        Task<Pedido> GetPedidoCompletoAsync(int id);

        Task<bool> ExistePedidoAsync(int id);
    }
}
