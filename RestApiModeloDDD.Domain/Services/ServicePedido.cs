using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Services
{
    public class ServicePedido : ServiceBase<Pedido>, IServicePedido
    {
        private readonly IRepositoryPedido _pedidoRepository;

        public ServicePedido(IRepositoryPedido pedidoRepository)
            : base(pedidoRepository)
        {
           this._pedidoRepository = pedidoRepository;
        }

      

        public async Task<List<Pedido>> GetPedidosCompletosAsync()
        {
            var pedidos = await _pedidoRepository.GetPedidosCompletosAsync();
            return pedidos;
        }
    }
}
