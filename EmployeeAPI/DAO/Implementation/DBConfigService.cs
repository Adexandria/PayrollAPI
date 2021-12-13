using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EmployeeAPI.DAO.Implementation
{
    public abstract class DBConfigService
    {
        protected IConfiguration configuration;

        protected DBConfigService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(configuration.GetValue<string>("ServiceDBConnectionString"));
            }
        }

        protected IDbConnection StaticConnection
        {
            get
            {
                return new SqlConnection(configuration.GetValue<string>("StaticConn"));
            }
        }
    }
}
