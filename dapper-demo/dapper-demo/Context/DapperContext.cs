using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Context
{
    public class DapperContext: IDapperContext
    {
        private readonly ContextOptions contextOptions;
        public DapperContext(IOptions<ContextOptions> options)
        {
            this.contextOptions = options.Value;
        }

        public IDbConnection CreateConnection() => new SqlConnection(contextOptions.ConnectionString);
    }
}
