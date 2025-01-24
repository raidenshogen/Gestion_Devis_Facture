using Gestion_Devis_Facture.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Gestion_Devis_Facture.Services
{
    public class QuotePdfService
    {
        private readonly IWebHostEnvironment _environment;

        public QuotePdfService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public byte[] GenerateQuotePdf(Quote quote)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 36, 36, 72, 36);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();

                // Add company logo
                string logoPath = Path.Combine(_environment.WebRootPath, "images", "logo.png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(100f, 100f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                }

                // Add company name
                Font titleFont = new Font(Font.FontFamily.HELVETICA, 16, Font.NORMAL);
                Paragraph companyName = new Paragraph("COMPANY NAME", titleFont);
                companyName.Alignment = Element.ALIGN_CENTER;
                companyName.SpacingAfter = 40f;
                document.Add(companyName);

                // Add billing information
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 1f });

                // Left side - Client info
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                leftCell.AddElement(new Paragraph("ISSUED TO:", new Font(Font.FontFamily.HELVETICA, 8)));
                leftCell.AddElement(new Paragraph(quote.Agent.Name));
                leftCell.AddElement(new Paragraph(quote.Agent.Address));
                headerTable.AddCell(leftCell);

                // Right side - Quote details
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                rightCell.AddElement(new Paragraph($"QUOTE NO: {quote.QuoteNumber}"));
                rightCell.AddElement(new Paragraph($"DATE: {quote.Date:dd.MM.yyyy}"));
                rightCell.AddElement(new Paragraph($"VALID UNTIL: {quote.Date.AddDays(30):dd.MM.yyyy}")); // Example: 30 days validity
                headerTable.AddCell(rightCell);

                document.Add(headerTable);
                document.Add(new Paragraph("\n"));

                // Add items table
                PdfPTable itemsTable = new PdfPTable(4);
                itemsTable.WidthPercentage = 100;
                itemsTable.SetWidths(new float[] { 2f, 1f, 1f, 1f });

                // Table headers
                Font headerFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD);
                itemsTable.AddCell(new PdfPCell(new Phrase("DESCRIPTION", headerFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 8 });
                itemsTable.AddCell(new PdfPCell(new Phrase("PRICE", headerFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 8 });
                itemsTable.AddCell(new PdfPCell(new Phrase("QTY", headerFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 8 });
                itemsTable.AddCell(new PdfPCell(new Phrase("TOTAL", headerFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 8 });

                // Add items
                foreach (var item in quote.Items)
                {
                    itemsTable.AddCell(new PdfPCell(new Phrase(item.Description)) { Padding = 8 });
                    itemsTable.AddCell(new PdfPCell(new Phrase($"${item.UnitPrice:N2}")) { Padding = 8 });
                    itemsTable.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString())) { Padding = 8 });
                    itemsTable.AddCell(new PdfPCell(new Phrase($"${item.Quantity * item.UnitPrice:N2}")) { Padding = 8 });
                }

                document.Add(itemsTable);

                // Add totals
                PdfPTable totalsTable = new PdfPTable(2);
                totalsTable.WidthPercentage = 100;
                totalsTable.SetWidths(new float[] { 3f, 1f });

                totalsTable.AddCell(new PdfPCell(new Phrase("SUBTOTAL")) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                totalsTable.AddCell(new PdfPCell(new Phrase($"${quote.TotalAmount:N2}")) { Border = Rectangle.NO_BORDER });

                decimal tax = quote.TotalAmount * 0.1m; // 10% tax example
                totalsTable.AddCell(new PdfPCell(new Phrase("TAX (10%)")) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                totalsTable.AddCell(new PdfPCell(new Phrase($"${tax:N2}")) { Border = Rectangle.NO_BORDER });

                totalsTable.AddCell(new PdfPCell(new Phrase("TOTAL", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                totalsTable.AddCell(new PdfPCell(new Phrase($"${(quote.TotalAmount + tax):N2}", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { Border = Rectangle.NO_BORDER });

                document.Add(totalsTable);

                // Add signature section
                document.Add(new Paragraph("\n\n"));
                Paragraph signature = new Paragraph("SIGNATURE", new Font(Font.FontFamily.HELVETICA, 8));
                signature.Alignment = Element.ALIGN_RIGHT;
                document.Add(signature);

                document.Close();
                return ms.ToArray();
            }
        }
    }
}