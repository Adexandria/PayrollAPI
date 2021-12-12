using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class SqlService
    {
        string connectionString;
        readonly IConfiguration _config;

        public SqlService(IConfiguration _config)
        {
            this._config = _config;
        }

        public SqlConnection CreateConnection()
        {
            //Add connection Name
            connectionString = _config["ConnectionStrings:{connectionName}"];
            var sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }
    }
}
