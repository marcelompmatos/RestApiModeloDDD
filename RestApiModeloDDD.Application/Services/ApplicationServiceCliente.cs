using AutoMapper;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Domain.Exceptions;
using RestApiModeloDDD.Domain.Validations;
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

        public ApplicationServiceCliente(
            IServiceCliente serviceCliente,
            IMapper mapper,
            ILogger<ApplicationServiceCliente> logger)
        {
            this.serviceCliente = serviceCliente;
            this.mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando cadastro de cliente na camada Application. Nome: {NomeCliente}",
                clienteDto?.Nome);

            ValidarClienteDto(clienteDto);

            var cliente = mapper.Map<Cliente>(clienteDto);

            ValidarEntidade(cliente);

            await serviceCliente.AddAsync(cliente);

            _logger.LogInformation(
                "Cliente cadastrado com sucesso na camada Application. Nome: {NomeCliente}",
                clienteDto.Nome);
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
            var cliente = await serviceCliente.GetByIdAsync(id);

            if (cliente == null)
                throw new NotFoundException(
                    $"Cliente com Id {id} não encontrado.");

            return mapper.Map<ClienteDto>(cliente);
        }

        public async Task RemoveAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando remoção de cliente na camada Application. Id: {ClienteId}",
                id);

            if (id <= 0)
            {
                _logger.LogWarning(
                    "Id inválido informado para remoção. Id: {ClienteId}",
                    id);

                throw new DomainValidationException(
                    "O Id do cliente deve ser maior que zero.");
            }

            await serviceCliente.RemoveAsync(id);

            _logger.LogInformation(
                "Cliente removido com sucesso na camada Application. Id: {ClienteId}",
                id);
        }

        public async Task UpdateAsync(ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando atualização de cliente na camada Application. Id: {ClienteId}",
                clienteDto?.Id);

            ValidarClienteDto(clienteDto);

            if (clienteDto.Id <= 0)
            {
                _logger.LogWarning(
                    "Tentativa de atualização com Id inválido. Id: {ClienteId}",
                    clienteDto.Id);

                throw new DomainValidationException("O Id do cliente deve ser maior que zero.");
            }

            var clienteExistente =
                await serviceCliente.GetByIdAsync(clienteDto.Id);

            if (clienteExistente == null)
            {
                _logger.LogWarning(
                    "Cliente não encontrado para atualização. Id: {ClienteId}",
                    clienteDto.Id);

                throw new NotFoundException(
                    $"Cliente com Id {clienteDto.Id} não encontrado.");
            }

            var cliente = mapper.Map<Cliente>(clienteDto);

            ValidarEntidade(cliente);

            await serviceCliente.UpdateAsync(cliente);

            _logger.LogInformation(
                "Cliente atualizado com sucesso na camada Application. Id: {ClienteId}",
                clienteDto.Id);
        }

        private void ValidarClienteDto(ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                _logger.LogWarning(
                    "ClienteDto recebido é nulo");

                throw new DomainValidationException("Os dados do cliente são obrigatórios.");
            }
        }

        private void ValidarEntidade(Cliente cliente)
        {
            var validation = new ClienteValidation();

            var result = validation.Validate(cliente);

            if (!result.IsValid)
            {
                var erros = result.Errors
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

                _logger.LogWarning(
                    "Erro de validação da entidade Cliente. Erros: {Erros}",
                    string.Join(" | ", erros));

                throw new DomainValidationException( result.Errors.Select(x => x.ErrorMessage));
            }
        }
    }
}