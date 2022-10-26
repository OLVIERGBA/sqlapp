using sqlapp.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductServices
    {
        private static string db_source   = "srvapp.database.windows.net";
        private static string db_user     = "admindba";
        private static string db_password = "Administration2022!";
        private static string db_databae = "srvdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_databae;
            return new SqlConnection(_builder.ConnectionString);
        
        }

        public List<Products>GetProducts()
        {
            SqlConnection conn = GetConnection();
            
            List < Products > _list_product = new List<Products>();
            
            string statement = "Select ProductID, ProductName, Quantity  from Produts";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Products products = new Products()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _list_product.Add(products);
                }
            }
            conn.Close();
            return _list_product;
        }

    }
}
