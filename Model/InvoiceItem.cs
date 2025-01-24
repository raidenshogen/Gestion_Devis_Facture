namespace Gestion_Devis_Facture.Model
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ?Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Product { get; set; }
        public virtual Invoice ?Invoice { get; set; }
    }
}
