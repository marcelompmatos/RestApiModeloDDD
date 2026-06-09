using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Infrastructure.Data.Context;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryProduto : RepositoryBase<Produto>, IRepositoryProduto
    {
        private readonly SqlContext sqlContext;

        public RepositoryProduto(SqlContext sqlContext, 
            ILogger<RepositoryBase<Produto>> logger)
            : base(sqlContext, logger)
        {
            this.sqlContext = sqlContext;
        }
    }
}