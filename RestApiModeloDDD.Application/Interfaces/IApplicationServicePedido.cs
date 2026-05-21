using RestApiModeloDDD.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServicePedido
    {
        Task<List<PedidoDto>> GetPedidosAsync();
        Task<PedidoDto> GetPedidoAsync(int id);
    }
}
