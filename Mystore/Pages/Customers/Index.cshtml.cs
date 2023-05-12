using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> CustomerList = new List<CustomerInfo> ();
        public void OnGet()
        {
            try {
                String connectionString = "Data Source=.\\CAREMY;Initial Catalog=Mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open ();
                    string sql = "SELECT *FROM Customers";
                   using(SqlCommand command=new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader ())
                        {
                            while(reader.Read ())
                            {
                                CustomerInfo customerInfo = new CustomerInfo ();
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.name =  reader.GetString(1);
                                customerInfo.email = reader.GetString(2);
                                customerInfo.phone =reader.GetString(3);
                                customerInfo.address=reader.GetString(4);
                                customerInfo.created_at=reader.GetDateTime(5).ToString();
                                CustomerList .Add (customerInfo);

                            }
                        }
                    }
                        
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception"+ ex.ToString());
            }
        }
    }
    public class CustomerInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
    //allows us to store data of only one customer
}
