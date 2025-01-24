using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestion_Devis_Facture.Services;
using Gestion_Devis_Facture.Model;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gestion_Devis_Facture.Pages.Agents
{
    [Authorize(Roles = "client")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Agent Agn { get; set; } = new Agent();

        public string errorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            

            try
            {
                string Connect = "Data Source=DESKTOP-PSL7GKI;Initial Catalog=InvoiceDb;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(Connect))
                {
                    con.Open();
                    string sql = "INSERT INTO Ags (Name, Address, Email, Phone, UserId) VALUES (@Name, @Address, @Email, @Phone, @UserId)";

                    using (SqlCommand insert = new SqlCommand(sql, con))
                    {
                        // Get the authenticated user's ID
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        // Set the UserId for the new client
                        Agn.UserId = userId;

                        // Add parameters
                        insert.Parameters.AddWithValue("@Name", Agn.Name);
                        insert.Parameters.AddWithValue("@Address", Agn.Address);
                        insert.Parameters.AddWithValue("@Email", Agn.Email);
                        insert.Parameters.AddWithValue("@Phone", Agn.Phone);
                        insert.Parameters.AddWithValue("@UserId", Agn.UserId);

                        // Execute the query
                        insert.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "Client added successfully!";
                return RedirectToPage("./Agents_list");
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while saving the client.";
                Console.WriteLine("Exception: " + ex.ToString());
                return Page();
            }
        }
    }
}
