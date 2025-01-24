namespace Gestion_Devis_Facture.Model
{
    public class InvoiceTemplate
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?HtmlTemplate { get; set; }
        public string? CssStyles { get; set; }
        public string ?PreviewImageUrl { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ?CreatedBy { get; set; }
    }
}
