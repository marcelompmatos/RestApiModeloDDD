using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Services;
using RestApiModeloDDD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IApplicationServicePedido _applicationServicePedido;
        public PedidoController(IApplicationServicePedido applicationServicePedido)
        {
            this._applicationServicePedido = applicationServicePedido;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var pedidos = await _applicationServicePedido.GetPedidosCompletosAsync();
            return Ok(pedidos);
        }

       
    }
}
