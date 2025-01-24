namespace Gestion_Devis_Facture.Model
{
    public class QuoteItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice; // Computed property
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}
