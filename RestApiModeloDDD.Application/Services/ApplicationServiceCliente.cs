using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceCliente : IApplicationServiceCliente
    {
        private readonly IServiceCliente serviceCliente;
        private readonly IMapper mapper;
        public ApplicationServiceCliente(IServiceCliente serviceCliente
                                       , IMapper mapper)
        {
            this.serviceCliente = serviceCliente;
            this.mapper = mapper;
        }

        public async Task AddAsync(ClienteDto clienteDto)
        {
            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.AddAsync(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await serviceCliente.GetAllAsync();

            return mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await serviceCliente.GetByIdAsync(id);

            return mapper.Map<ClienteDto>(cliente);
        }

        public async Task RemoveAsync(ClienteDto clienteDto)
        {
            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.RemoveAsync(cliente);
        }

        public async Task UpdateAsync(ClienteDto clienteDto)
        {
            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.UpdateAsync(cliente);
        }


    }
}
