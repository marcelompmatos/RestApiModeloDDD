using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Application.Mappers
{
    public class DtoToModelMappingPedido : Profile
    {
        public DtoToModelMappingPedido()
        {
            PedidoMap();
        }


        private void PedidoMap()
        {
            CreateMap<PedidoDto, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.DataPedido, opt => opt.MapFrom(src => src.DataPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
               // .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore());
        }
    }
}