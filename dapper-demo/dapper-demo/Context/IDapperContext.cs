using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Context
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
