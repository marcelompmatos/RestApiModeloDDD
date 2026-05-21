using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServicePedido : IApplicationServicePedido
    {
        private readonly IServicePedido _servicePedido;
        private readonly IMapper mapper;

        public ApplicationServicePedido(IServicePedido servicePedido,
                                        IMapper mapper)
        {
            this._servicePedido = servicePedido;
            this.mapper = mapper;
        }

        public async Task<PedidoDto> GetPedidoAsync(int id)
        {
            var pedido = await _servicePedido.GetPedidoAsync(id);

            if (pedido == null)
                return null;

            return new PedidoDto
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,

                Cliente = pedido.Cliente == null ? null : new ClienteDto
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

                    Produto = i.Produto == null ? null : new ProdutoDto
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
            var pedidos = await _servicePedido.GetPedidosAsync();

            return pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                DataPedido = p.DataPedido,
                ValorTotal = p.ValorTotal,

                Cliente = p.Cliente == null ? null : new ClienteDto
                {
                    Id = p.Cliente.Id,
                    Nome = p.Cliente.Nome,
                    Sobrenome = p.Cliente.Sobrenome,
                    Email = p.Cliente.Email
                },
            }).ToList();
        }
    }
}