using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Customers
{
    public class EditModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";


        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\CAREMY;Initial Catalog=Mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Customers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.name = "" + reader.GetString(1);
                                customerInfo.email = "" + reader.GetString(2);
                                customerInfo.phone = "" + reader.GetString(3);
                          
                                
                                customerInfo.address = "" + reader.GetString(4);

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        //allow us to update data to database
        public void OnPost()
        {
            customerInfo.id = Request.Form["id"];
            customerInfo.name = Request.Form["name"];
            customerInfo.email = Request.Form["email"];
            customerInfo.phone = Request.Form["phone"];
            customerInfo.address = Request.Form["address"];
            if (customerInfo.name.Length == 0 || customerInfo.email.Length == 0
                || customerInfo.phone.Length == 0 || customerInfo.address.Length == 0)
            {
                ErrorMessage = "All fields are required";
                return;

            }

            try
            {
                String connectionString = "Data Source=.\\CAREMY;Initial Catalog=Mystore;Integrated Security=True";
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Customers" +
                        "SET name=@name, email=@email, phone=@phone, address=@address " +
                        "WHERE id=@id";
                   using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@email", customerInfo.email);
                        command.Parameters.AddWithValue("@phone", customerInfo.phone);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@id", customerInfo.id);
                        command.ExecuteNonQuery();
                           
                  
                    }
                }

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Customers/Index");

        }
    }
}
