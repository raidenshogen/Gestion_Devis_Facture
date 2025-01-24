using Gestion_Devis_Facture.Data;
using Gestion_Devis_Facture.Model;
using Gestion_Devis_Facture.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gestion_Devis_Facture.Pages
{
    [Authorize(Roles = "client")]
    public class Generate_InvModel : PageModel
    {
        private readonly InvoiceDbContext _context;

        private readonly InvoicePdfService _pdfService;
        
        
        public Generate_InvModel(InvoiceDbContext context, InvoicePdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }
        public IList<Invoice> Invoices { get; set; }
        public IList<Agent> Agents { get; set; }

        [BindProperty]
        public CreateInvoiceModel Input { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Invoices = await _context.Invoice

                .Include(i => i.Ag)
                .Include(i => i.Items)
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            Agents = await _context.Ags
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<JsonResult> OnGetAgentDetailsAsync(int agentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agent = await _context.Ags.FirstOrDefaultAsync(a => a.Id == agentId && a.UserId == userId);
            if (agent == null)
            {
                return new JsonResult(null);
            }

            return new JsonResult(new
            {
                agent.Id,
                agent.Name,
                agent.Address,
                agent.Email,
                agent.Phone
            });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Recharger les données nécessaires
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var invoice = new Invoice
            {
                InvoiceNumber = GenerateInvoiceNumber(),
                Date = DateTime.Now,
                AgentId = Input.AgentId,
                UserId = userId,
                Items = Input.Items.Select(item => new InvoiceItem
                {
                    Description = item.Description,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = item.Description
                }).ToList(),

            };
            invoice.Status = Input.Status;
            invoice.TotalAmount = invoice.Items.Sum(item => item.Quantity * item.Price);

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd}-{DateTime.Now.Ticks % 1000:000}";
        }
        public async Task<IActionResult> OnPostDownloadPdfAsync(int invoiceId)
        {
            var invoice = await _context.Invoice
                .Include(i => i.Ag)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            byte[] pdfBytes = _pdfService.GenerateInvoicePdf(invoice);
            return File(pdfBytes, "application/pdf", $"Invoice_{invoice.InvoiceNumber}.pdf");
        }

    }

    public class CreateInvoiceModel
    {
        public string ?Status { get; set; }

        public int AgentId { get; set; }
        public List<CreateInvoiceItemModel> Items { get; set; } = new List<CreateInvoiceItemModel>();
    }

    public class CreateInvoiceItemModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}