using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess.SqlServer
{
    //Conexión principal a la base de Datos
    public abstract class ConexionSQL
    {
        private readonly string connectionString;
        public ConexionSQL()
        {
            connectionString = "server=localhost; database=SMAPAM; user=root; pwd=root";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
