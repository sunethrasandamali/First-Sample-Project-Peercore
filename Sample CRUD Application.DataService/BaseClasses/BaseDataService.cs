using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sample_CRUD_Application.DataService.BaseClasses
{
    public class BaseDataService
    {
        private readonly IConfiguration _configuration;

        public BaseDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetDBConnection()
        {
            string connectionString = _configuration.GetConnectionString("CrudAppDB");
            return new SqlConnection(connectionString);
        }
    }
}
