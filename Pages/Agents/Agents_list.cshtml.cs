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
    public class Agents_listModel : PageModel
    {
        public List<Agent> listAgents { get; set; } = new List<Agent>();

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the authenticated user's ID

            try
            {
                string Connect = "Data Source=DESKTOP-PSL7GKI;Initial Catalog=InvoiceDb;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(Connect))
                {
                    con.Open();
                    string sql = "SELECT * FROM Ags WHERE UserId = @UserId"; // Filter by UserId

                    using (SqlCommand read = new SqlCommand(sql, con))
                    {
                        read.Parameters.AddWithValue("@UserId", userId); // Add the UserId parameter

                        using (SqlDataReader rd = read.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                Agent ci = new Agent();
                                ci.Id = rd.GetInt32(0);
                                ci.Name = rd.GetString(1);
                                ci.Address = rd.GetString(2);
                                ci.Email = rd.GetString(3);
                                ci.Phone = rd.GetString(4);
                                ci.UserId = rd.GetString(5); // Ensure UserId is mapped
                                listAgents.Add(ci);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and display a user-friendly message
                Console.WriteLine("Exception: " + ex.ToString());
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the list of clients.");
            }
        }
    }

}


