using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IApplicationServiceCliente applicationServiceCliente;
        ILogger<ClientesController> _logger;

        public ClientesController(IApplicationServiceCliente applicationServiceCliente,  ILogger<ClientesController> logger)
        {
            this.applicationServiceCliente = applicationServiceCliente;
            this._logger = logger;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            _logger.LogInformation("Iniciando consulta de clientes");

            var clientes = await applicationServiceCliente.GetAllAsync();

            var listaClientes = clientes.ToList();

            _logger.LogInformation("Consulta finalizada. Total de clientes encontrados: {Quantidade}",listaClientes.Count);

            if (!listaClientes.Any())
            {
                _logger.LogWarning("Nenhum cliente encontrado");

                return NoContent();
            }

            return Ok(listaClientes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            _logger.LogInformation(
          "Iniciando consulta do cliente. Id: {ClienteId}",
          id);

            var cliente = await applicationServiceCliente
                .GetByIdAsync(id);

            if (cliente == null)
            {
                _logger.LogWarning(
                    "Cliente não encontrado. Id: {ClienteId}",
                    id);

                return NotFound();
            }

            _logger.LogInformation(
                "Cliente encontrado com sucesso. Id: {ClienteId}",
                id);

            return Ok(cliente);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDto clienteDTO)
        {
            _logger.LogInformation(
                "Iniciando cadastro de cliente");

            if (clienteDTO == null)
            {
                _logger.LogWarning(
                    "Tentativa de cadastro com payload nulo");

                return BadRequest();
            }

            await applicationServiceCliente.AddAsync(clienteDTO);

            _logger.LogInformation(
                "Cliente cadastrado com sucesso");

            return Ok("Cliente cadastrado com sucesso!");
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando atualização do cliente. Id: {ClienteId}",
                    clienteDTO?.Id);

                if (clienteDTO == null)
                {
                    _logger.LogWarning(
                        "Tentativa de atualização com payload nulo");

                    return BadRequest(new
                    {
                        erro = "Os dados do cliente são obrigatórios."
                    });
                }

                await applicationServiceCliente.UpdateAsync(clienteDTO);

                _logger.LogInformation(
                    "Cliente atualizado com sucesso. Id: {ClienteId}",
                    clienteDTO.Id);

                return Ok(new
                {
                    mensagem = "Cliente atualizado com sucesso!"
                });
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(
                    "Erro de validação ao atualizar cliente. Erro: {Erro}",
                    ex.Message);

                return BadRequest(new
                {
                    erro = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(
                    "Cliente não encontrado para atualização. Erro: {Erro}",
                    ex.Message);

                return NotFound(new
                {
                    erro = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro interno ao atualizar cliente");

                return StatusCode(500, new
                {
                    erro = "Erro interno"
                });
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] ClienteDto clienteDTO)
        {
            _logger.LogInformation(
                "Iniciando remoção do cliente. Id: {ClienteId}",
                clienteDTO?.Id);

            if (clienteDTO == null)
            {
                _logger.LogWarning(
                    "Tentativa de remoção com payload nulo");

                return BadRequest();
            }

            await applicationServiceCliente.RemoveAsync(clienteDTO);

            _logger.LogInformation(
                "Cliente removido com sucesso. Id: {ClienteId}",
                clienteDTO.Id);

            return Ok(new
            {
                mensagem = "Cliente removido com sucesso!"
            });
        }
    }
}