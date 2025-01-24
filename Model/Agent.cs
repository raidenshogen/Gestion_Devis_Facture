using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Devis_Facture.Model
{
    public class Agent
    {
            
            public int Id { get; set; }
            [Required]
            
            [StringLength(100)]
            public string ?Name { get; set; }
            [Required]
            
            [StringLength(200)]
            public string? Address { get; set; }
            [Required]
            
            [EmailAddress]
            [StringLength(100)]
            public string ?Email { get; set; }
            [Required]
            
            [Phone]
            [StringLength(20)]
            public string ?Phone { get; set; }
            public string UserId { get; set; }

    public virtual ICollection<Invoice> ?Invoices { get; set; }
   

        

    }
}
