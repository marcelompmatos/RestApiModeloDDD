using AutoMapper;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Exceptions;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServicePedido : IApplicationServicePedido
    {
        private readonly IServicePedido _servicePedido;
        private readonly IServiceProduto _serviceProduto;
        private readonly IMapper mapper;
        private readonly ILogger<ApplicationServicePedido> _logger;

        public ApplicationServicePedido(IServicePedido servicePedido
                                      , IServiceProduto serviceProduto
                                      , IMapper mapper
                                      , ILogger<ApplicationServicePedido> logger
                                        )
        {
            this._servicePedido = servicePedido;
            this.mapper = mapper;
            this._logger = logger;
            this._serviceProduto = serviceProduto;
        }

        public async Task<PedidoDto> GetPedidoAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta de pedido na camada Application. PedidoId: {PedidoId}",
                id);

            var pedido = await _servicePedido
                .GetPedidoAsync(id);

            if (pedido == null)
            {
                _logger.LogWarning(
                    "Pedido não encontrado na camada Application. PedidoId: {PedidoId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Pedido encontrado com sucesso na camada Application. PedidoId: {PedidoId}",
                id);

            return new PedidoDto
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,

                Cliente = pedido.Cliente == null
                    ? null
                    : new ClienteDto
                    {
                        Id = pedido.Cliente.Id,
                        Nome = pedido.Cliente.Nome,
                        Sobrenome = pedido.Cliente.Sobrenome,
                        Email = pedido.Cliente.Email
                    },

                Itens = pedido.Itens.Select(i => new ItemPedidoDTO
                {
                    Id = i.Id,
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,

                    Produto = i.Produto == null
                        ? null
                        : new ProdutoDto
                        {
                            Id = i.Produto.Id,
                            Nome = i.Produto.Nome,
                            Valor = i.Produto.Valor
                        }

                }).ToList()
            };
        }

        public async Task<List<PedidoDto>> GetPedidosAsync()
        {
            _logger.LogInformation(
                "Iniciando consulta de pedidos na camada Application");

            var pedidos = await _servicePedido
                .GetPedidosAsync();

            var listaPedidos = pedidos.ToList();

            _logger.LogInformation(
                "Consulta de pedidos finalizada na camada Application. Quantidade: {Quantidade}",
                listaPedidos.Count);

            return listaPedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                DataPedido = p.DataPedido,
                ValorTotal = p.ValorTotal,

                Cliente = p.Cliente == null
                    ? null
                    : new ClienteDto
                    {
                        Id = p.Cliente.Id,
                        Nome = p.Cliente.Nome,
                        Sobrenome = p.Cliente.Sobrenome,
                        Email = p.Cliente.Email
                    }

            }).ToList();
        }


        public async Task AddAsync(CriarPedidoDto dto)
        {
            var itensPedido = new List<ItemPedido>();

            foreach (var item in dto.Itens)
            {
                var produto = await _serviceProduto.GetByIdAsync(item.ProdutoId);

                if (produto == null)
                    throw new DomainValidationException(
                        $"Produto {item.ProdutoId} não encontrado.");

                if (!produto.IsDisponivel)
                    throw new DomainValidationException(
                        $"Produto {produto.Nome} está indisponível.");

                itensPedido.Add(new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    ValorUnitario = produto.Valor
                });
            }

            var pedido = new Pedido
            {
                ClienteId = dto.ClienteId,
                DataPedido = DateTime.Now,
                Itens = itensPedido
            };

            pedido.ValorTotal = pedido.Itens.Sum(x =>
                x.Quantidade * x.ValorUnitario);

            await _servicePedido.AddAsync(pedido);

           
        }

    }
}