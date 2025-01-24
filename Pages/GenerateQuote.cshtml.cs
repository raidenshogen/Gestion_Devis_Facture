using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gestion_Devis_Facture.Data;
using Gestion_Devis_Facture.Model;
using Gestion_Devis_Facture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Pages
{
    [Authorize(Roles = "client")]
    public class GenerateQuoteModel : PageModel
    {
        private readonly QuoteDbContext _context;
        private readonly QuotePdfService _pdfService;
        private readonly InvoiceDbContext _invoiceContext;


        public GenerateQuoteModel(QuoteDbContext context, QuotePdfService pdfService, InvoiceDbContext invoiceContext)
        {
            _context = context;
            _pdfService = pdfService;
            _invoiceContext = invoiceContext;
        }

        public IList<Quote> Quotes { get; set; }
        public IList<Agent> Agents { get; set; }

        [BindProperty]
        public CreateQuoteModel Input { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Quotes = await _context.Quotes
                .Include(q => q.Agent)
                .Include(q => q.Items)
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.Date)
                .ToListAsync();

            Agents = await _invoiceContext.Ags
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<JsonResult> OnGetAgentDetailsAsync(int agentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agent = await _invoiceContext.Ags.FirstOrDefaultAsync(a => a.Id == agentId && a.UserId == userId);
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
                await OnGetAsync(); // Reload necessary data
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"Selected AgentId: {Input.AgentId}");

            // Validate the AgentId
            var agentExists = await _invoiceContext.Ags.AnyAsync(a => a.Id == Input.AgentId && a.UserId == userId);
            if (!agentExists)
            {
                ModelState.AddModelError("Input.AgentId", "Invalid Agent selected.");
                await OnGetAsync(); // Reload necessary data
                return Page();
            }

            var quote = new Quote
            {
                QuoteNumber = GenerateQuoteNumber(),
                Date = DateTime.Now,
                AgentId = Input.AgentId, // Ensure this value is valid
                UserId = userId,
                Items = Input.Items.Select(item => new QuoteItem
                {
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                Status = Input.Status,
                TotalAmount = Input.Items.Sum(item => item.Quantity * item.UnitPrice)
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private string GenerateQuoteNumber()
        {
            return $"QUO-{DateTime.Now:yyyyMMdd}-{DateTime.Now.Ticks % 1000:000}";
        }

        public async Task<IActionResult> OnPostDownloadPdfAsync(int quoteId)
        {
            var quote = await _context.Quotes
                .Include(q => q.Agent)
                .Include(q => q.Items)
                .FirstOrDefaultAsync(q => q.Id == quoteId);

            if (quote == null)
            {
                return NotFound();
            }

            byte[] pdfBytes = _pdfService.GenerateQuotePdf(quote);
            return File(pdfBytes, "application/pdf", $"Quote_{quote.QuoteNumber}.pdf");
        }
    }

    public class CreateQuoteModel
    {
        public string? Status { get; set; }
        public int AgentId { get; set; }
        public List<CreateQuoteItemModel> Items { get; set; } = new List<CreateQuoteItemModel>();
    }

    public class CreateQuoteItemModel
    {
        public string ?Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}