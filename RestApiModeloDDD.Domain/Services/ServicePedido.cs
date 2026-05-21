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

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoAsync(id);
            return pedido;
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            var pedidos = await _pedidoRepository.GetPedidosAsync();
            return pedidos;
        }
    }
}
