using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PedidoController : ControllerBase
{
    private readonly IApplicationServicePedido _applicationServicePedido;

    public PedidoController(
        IApplicationServicePedido applicationServicePedido)
    {
        _applicationServicePedido = applicationServicePedido;
    }

    /// <summary>
    /// Cria um novo pedido.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] CriarPedidoDto dto,
        CancellationToken cancellationToken)
    {
        var pedidoId =
            await _applicationServicePedido.AddAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = pedidoId },
            new
            {
                Id = pedidoId,
                Message = "Pedido criado com sucesso."
            });
    }

    /// <summary>
    /// Retorna todos os pedidos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var pedidos =
            await _applicationServicePedido.GetPedidosAsync();

        if (!pedidos.Any())
            return NoContent();

        return Ok(pedidos);
    }

    /// <summary>
    /// Retorna um pedido pelo Id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PedidoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var pedido =
            await _applicationServicePedido.GetPedidoAsync(id);

        if (pedido is null)
            return NotFound();

        return Ok(pedido);
    }
}