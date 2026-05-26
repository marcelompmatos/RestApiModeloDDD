using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.API.Observability;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Entities;
using Serilog;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
           
            _logger.LogInformation( "Consultando todos os pedidos");

            var pedidos = await _applicationServicePedido.GetPedidosAsync();
                      
            _logger.LogInformation("Total de pedidos encontrados: {Quantidade}", pedidos.Count());

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> GetById(int id)
        {
            _logger.LogInformation("Consultando pedido {PedidoId}",id);

            var pedidos = await _applicationServicePedido.GetPedidoAsync(id);
           
            if (pedidos == null)
            {
                _logger.LogWarning("Pedido {PedidoId} não encontrado", id);
                return NotFound();
            }
    
            _logger.LogInformation("Pedido {PedidoId} encontrado", id);

            return Ok(pedidos);
        }

            [HttpGet("erro")]
            public IActionResult GerarErro()
            {
                throw new Exception("Erro proposital");
            }
        


    }
}
