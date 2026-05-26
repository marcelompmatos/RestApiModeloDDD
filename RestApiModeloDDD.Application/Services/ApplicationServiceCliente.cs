using AutoMapper;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceCliente : IApplicationServiceCliente
    {
        private readonly IServiceCliente serviceCliente;
        private readonly IMapper mapper;
        private readonly ILogger<ApplicationServiceCliente> _logger;
        public ApplicationServiceCliente(IServiceCliente serviceCliente
                                       , IMapper mapper , ILogger<ApplicationServiceCliente> logger)
        {
            this.serviceCliente = serviceCliente;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task AddAsync(ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando cadastro de cliente na camada Application. Nome: {NomeCliente}",
                clienteDto?.Nome);

            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.AddAsync(cliente);

            _logger.LogInformation(
                "Cliente cadastrado com sucesso na camada Application. Nome: {NomeCliente}",
                clienteDto?.Nome);
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            _logger.LogInformation(
                "Iniciando consulta de clientes na camada Application");

            var clientes = await serviceCliente.GetAllAsync();

            var listaClientes = clientes.ToList();

            _logger.LogInformation(
                "Consulta de clientes finalizada na camada Application. Quantidade: {Quantidade}",
                listaClientes.Count);

            return mapper.Map<IEnumerable<ClienteDto>>(listaClientes);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta de cliente por Id na camada Application. Id: {ClienteId}",
                id);

            var cliente = await serviceCliente.GetByIdAsync(id);

            if (cliente == null)
            {
                _logger.LogWarning(
                    "Cliente não encontrado na camada Application. Id: {ClienteId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Cliente encontrado com sucesso na camada Application. Id: {ClienteId}",
                id);

            return mapper.Map<ClienteDto>(cliente);
        }

        public async Task RemoveAsync(ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando remoção de cliente na camada Application. Id: {ClienteId}",
                clienteDto?.Id);

            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.RemoveAsync(cliente);

            _logger.LogInformation(
                "Cliente removido com sucesso na camada Application. Id: {ClienteId}",
                clienteDto?.Id);
        }

        public async Task UpdateAsync(ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando atualização de cliente na camada Application. Id: {ClienteId}",
                clienteDto?.Id);

            var cliente = mapper.Map<Cliente>(clienteDto);

            await serviceCliente.UpdateAsync(cliente);

            _logger.LogInformation(
                "Cliente atualizado com sucesso na camada Application. Id: {ClienteId}",
                clienteDto?.Id);
        }


    }
}
