using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.SqlLiteData
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();

    }
}
