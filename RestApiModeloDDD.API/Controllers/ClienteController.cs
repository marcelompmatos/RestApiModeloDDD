using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IApplicationServiceCliente _applicationServiceCliente;
        private readonly ILogger<ClientesController> _logger;


    public ClientesController(
        IApplicationServiceCliente applicationServiceCliente,
        ILogger<ClientesController> logger)
        {
            _applicationServiceCliente = applicationServiceCliente;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            _logger.LogInformation(
                "Iniciando consulta de clientes");

            var clientes = await _applicationServiceCliente.GetAllAsync();

            var listaClientes = clientes.ToList();

            _logger.LogInformation(
                "Consulta finalizada. Total de clientes encontrados: {Quantidade}",
                listaClientes.Count);

            if (!listaClientes.Any())
            {
                return NoContent();
            }

            return Ok(listaClientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta do cliente. Id: {ClienteId}",
                id);

            var cliente = await _applicationServiceCliente.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    erro = $"Cliente com Id {id} não encontrado."
                });
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando cadastro de cliente");

            await _applicationServiceCliente.AddAsync(clienteDto);

            _logger.LogInformation(
                "Cliente cadastrado com sucesso");

            return CreatedAtAction(
                nameof(Get),
                new { id = clienteDto.Id },
                clienteDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClienteDto clienteDto)
        {
            _logger.LogInformation(
                "Iniciando atualização do cliente. Id: {ClienteId}",
                clienteDto?.Id);

            await _applicationServiceCliente.UpdateAsync(clienteDto);

            _logger.LogInformation(
                "Cliente atualizado com sucesso. Id: {ClienteId}",
                clienteDto.Id);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation(
                "Iniciando remoção do cliente. Id: {ClienteId}",
                id);

            await _applicationServiceCliente.RemoveAsync(id);

            _logger.LogInformation(
                "Cliente removido com sucesso. Id: {ClienteId}",
                id);

            return NoContent();
        }
    }


}
