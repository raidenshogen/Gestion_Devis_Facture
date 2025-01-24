namespace Gestion_Devis_Facture.Model
{
    public class Quote
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; }
        public DateTime Date { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public string UserId { get; set; }
        public List<QuoteItem> Items { get; set; } = new List<QuoteItem>();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
