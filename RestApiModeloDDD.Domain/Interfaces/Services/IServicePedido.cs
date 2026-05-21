using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Services
{
    public interface IServicePedido : IServiceBase<Pedido>
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<Pedido> GetPedidoAsync(int id);

    }

}
