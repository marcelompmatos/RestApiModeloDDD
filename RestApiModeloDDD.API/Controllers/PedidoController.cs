using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RestApiModeloDDD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IApplicationServicePedido _applicationServicePedido;
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(IApplicationServicePedido applicationServicePedido, ILogger<PedidoController> logger)
        {
            this._applicationServicePedido = applicationServicePedido;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CriarPedidoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _applicationServicePedido.AddAsync(dto);
           


            _logger.LogInformation(
                "Pedido criado para o cliente {ClienteId}",
                dto.ClienteId);

            return Created();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            _logger.LogInformation("Consultando todos os pedidos");

            var pedidos = await _applicationServicePedido.GetPedidosAsync();

            if (!pedidos.Any())
            {
                _logger.LogWarning("Nenhum pedido encontrado");

                return NoContent();
            }

            _logger.LogInformation(
                "Total de pedidos encontrados: {Quantidade}",
                pedidos.Count());

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id inválido informado: {PedidoId}", id);

                return BadRequest(new
                {
                    message = "Id deve ser maior que zero"
                });
            }

            _logger.LogInformation("Consultando pedido {PedidoId}", id);

            var pedido = await _applicationServicePedido.GetPedidoAsync(id);

            if (pedido == null)
            {
                _logger.LogWarning("Pedido {PedidoId} não encontrado", id);

                return NotFound(new
                {
                    message = $"Pedido {id} não encontrado"
                });
            }

            _logger.LogInformation("Pedido {PedidoId} encontrado", id);

            return Ok(pedido);
        }
    }
}
