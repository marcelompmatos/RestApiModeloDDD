using Autofac;
using Autofac.Core;
using AutoMapper;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Mappers;
using RestApiModeloDDD.Application.Services;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using RestApiModeloDDD.Domain.Services;
using RestApiModeloDDD.Infrastructure.Data.Repositories;
using RestApiModeloDDD.Service.Services;

namespace RestApiModeloDDD.Infrastructure.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC

            builder.RegisterType<ApplicationServiceCliente>().As<IApplicationServiceCliente>();
            builder.RegisterType<RepositoryCliente>().As<IRepositoryCliente>();
            builder.RegisterType<ServiceCliente>().As<IServiceCliente>();

            builder.RegisterType<ApplicationServiceProduto>().As<IApplicationServiceProduto>();
            builder.RegisterType<RepositoryProduto>().As<IRepositoryProduto>();
            builder.RegisterType<ServiceProduto>().As<IServiceProduto>();

            builder.RegisterType<ApplicationServicePedido>().As<IApplicationServicePedido>();
            builder.RegisterType<RepositoryPedido>().As<IRepositoryPedido>();
            builder.RegisterType<ServicePedido>().As<IServicePedido>();

            builder.RegisterType<ApplicationServiceAuth>().As<IApplicationServiceAuth>();
            builder.RegisterType<RepositoryUsuario>().As<IRepositoryUsuario>();
            builder.RegisterType<ServiceAuth>().As<IServiceAuth>();

           
            builder.RegisterType<ApplicationServiceRefreshToken>().As<IApplicationServiceRefreshToken>();
            builder.RegisterType<RepositoryRefreshToken>().As<IRepositoryRefreshToken>();
            builder.RegisterType<JwtService>().As<IJwtService>();


            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelMappingCliente());
                cfg.AddProfile(new ModelToDtoMappingCliente());
                cfg.AddProfile(new DtoToModelMappingProduto());
                cfg.AddProfile(new ModelToDtoMappingProduto());

            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }

        #endregion IOC
    }

}