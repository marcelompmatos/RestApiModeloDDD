using System.Collections.Generic;

namespace RestApiModeloDDD.Application.Dtos
{
    public class CriarPedidoDto
    {
        public int ClienteId { get; set; }

        public ICollection<CriarItemPedidoDto> Itens { get; set; }
    }
}
