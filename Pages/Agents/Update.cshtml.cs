using Gestion_Devis_Facture.Model;
using Gestion_Devis_Facture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Claims;

namespace Gestion_Devis_Facture.Pages.Agents
{
    [Authorize(Roles = "client")]
    public class UpdateModel : PageModel
    {
        public Agent ci = new Agent();

        public void OnGet()
        {
            string id = Request.Query["Id"];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                string Connect = "Data Source=DESKTOP-PSL7GKI; Initial Catalog=InvoiceDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(Connect))
                {
                    con.Open();
                    string sql = "SELECT * FROM Ags WHERE id=@id AND UserId = @UserId";
                    using (SqlCommand update = new SqlCommand(sql, con))
                    {
                        update.Parameters.AddWithValue("@id", id);
                        update.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader rd = update.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                ci.Id = rd.GetInt32(0);
                                ci.Name = rd.GetString(1);
                                ci.Address = rd.GetString(2);
                                ci.Email = rd.GetString(3);
                                ci.Phone = rd.GetString(4);
                                ci.UserId = rd.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void OnPost()
        {
            ci.Id = Convert.ToInt32(Request.Form["Id"]);
            ci.Name = Request.Form["Name"];
            ci.Address = Request.Form["Address"];
            ci.Email = Request.Form["Email"];
            ci.Phone = Request.Form["Phone"];

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                string Connect = "Data Source=DESKTOP-PSL7GKI;Initial Catalog=InvoiceDb; Integrated Security=True";
                using (SqlConnection con = new SqlConnection(Connect))
                {
                    con.Open();

                    string sql = "UPDATE Ags SET Name = @Name, Address = @Address, Email = @Email, Phone = @Phone WHERE Id = @Id";
                    using (SqlCommand update = new SqlCommand(sql, con))
                    {
                        update.Parameters.AddWithValue("@Id", ci.Id);
                        update.Parameters.AddWithValue("@Name", ci.Name); 
                        update.Parameters.AddWithValue("@Address", ci.Address);
                        update.Parameters.AddWithValue("@Email", ci.Email);
                        update.Parameters.AddWithValue("@Phone", ci.Phone);
                       
                        update.ExecuteNonQuery();
                    }
                }
                Response.Redirect("./Agents_list");
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
