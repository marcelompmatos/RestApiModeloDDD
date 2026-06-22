using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IApplicationServiceCliente _applicationServiceCliente;

    public ClientesController(
        IApplicationServiceCliente applicationServiceCliente)
    {
        _applicationServiceCliente = applicationServiceCliente;
    }

    /// <summary>
    /// Retorna todos os clientes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClienteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll(CancellationToken cancellationToken)
    {
        var clientes =
            await _applicationServiceCliente.GetAllAsync();

        if (!clientes.Any())
            return NoContent();

        return Ok(clientes);
    }

    /// <summary>
    /// Retorna um cliente pelo Id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClienteDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var cliente =
            await _applicationServiceCliente.GetByIdAsync(id);

        if (cliente is null)
            return NotFound();

        return Ok(cliente);
    }

    /// <summary>
    /// Cadastra um novo cliente.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] ClienteDto clienteDto,CancellationToken cancellationToken)
    {
        var clienteId =
            await _applicationServiceCliente.AddAsync(clienteDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = clienteId },
            new
            {
                Id = clienteId,
                Message = "Cliente cadastrado com sucesso."
            });
    }

    /// <summary>
    /// Atualiza um cliente.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id,[FromBody] ClienteDto clienteDto, CancellationToken cancellationToken)
    {
        clienteDto.Id = id;

        await _applicationServiceCliente.UpdateAsync(clienteDto);

        return NoContent();
    }

    /// <summary>
    /// Remove um cliente.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _applicationServiceCliente.RemoveAsync(id);

        return NoContent();
    }
}