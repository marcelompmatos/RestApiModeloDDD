using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Application.Mappers
{
    public class ModelToDtoMappingPedido : Profile
    {
        public ModelToDtoMappingPedido()
        {
            PedidoDtoMap();
        }

        private void PedidoDtoMap()
        {
            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.DataPedido, opt => opt.MapFrom(src => src.DataPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal));
                //.ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));
        }
    }
}
