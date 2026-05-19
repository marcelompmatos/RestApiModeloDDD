using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<List<PedidoDto>> GetPedidosCompletosAsync()
        {
            var pedidos = await _servicePedido.GetPedidosCompletosAsync();

            return pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                DataPedido = p.DataPedido,
                ValorTotal = p.ValorTotal,

                Cliente = new ClienteDto
                {
                    Id = p.Cliente.Id,
                    Nome = p.Cliente.Nome,
                    Sobrenome = p.Cliente.Sobrenome,
                    Email = p.Cliente.Email
                },

                Itens = p.Itens.Select(i => new ItemPedidoDTO
                {
                    Id = i.Id,
                    
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario
                }).ToList()

            }).ToList();
        }
    }
}