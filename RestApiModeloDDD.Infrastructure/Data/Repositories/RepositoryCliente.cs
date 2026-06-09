using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Infrastructure.Data.Context;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryCliente : RepositoryBase<Cliente>, IRepositoryCliente
    {
        private readonly SqlContext _sqlContext;

        public RepositoryCliente(
            SqlContext sqlContext,
            ILogger<RepositoryBase<Cliente>> logger)
            : base(sqlContext, logger)
        {
            _sqlContext = sqlContext;
        }
    }
}