using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Services
{
    public class ServicePedido : ServiceBase<Pedido>, IServicePedido
    {
        private readonly IRepositoryPedido _pedidoRepository;
        private readonly ILogger<ServicePedido> _logger;

        public ServicePedido(IRepositoryPedido pedidoRepository, ILogger<ServicePedido> logger)
            : base(pedidoRepository)
        {
           this._pedidoRepository = pedidoRepository;
           this._logger = logger;

        }

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta de pedido na camada Domain. PedidoId: {PedidoId}",
                id);

            var pedido = await _pedidoRepository
                .GetPedidoAsync(id);

            if (pedido == null)
            {
                _logger.LogWarning(
                    "Pedido não encontrado na camada Domain. PedidoId: {PedidoId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Pedido encontrado com sucesso na camada Domain. PedidoId: {PedidoId}",
                id);

            return pedido;
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            _logger.LogInformation(
                "Iniciando consulta de pedidos na camada Domain");

            var pedidos = await _pedidoRepository
                .GetPedidosAsync();

            _logger.LogInformation(
                "Consulta de pedidos finalizada na camada Domain. Quantidade: {Quantidade}",
                pedidos.Count);

            return pedidos;
        }

        public async Task AddAsync(Pedido pedido)
        {
            _logger.LogInformation(
                "Iniciando cadastro de pedido. ClienteId: {ClienteId}",
                pedido.ClienteId);

            if (pedido == null)
            {
                _logger.LogWarning(
                    "Tentativa de cadastro de pedido nulo");

                throw new ArgumentNullException(nameof(pedido));
            }

            await _pedidoRepository.AddAsync(pedido);

            _logger.LogInformation(
                "Pedido cadastrado com sucesso. PedidoId: {PedidoId}",
                pedido.Id);
        }
    }
}
