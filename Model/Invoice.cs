namespace Gestion_Devis_Facture.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public int AgentId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime ?DueDate { get; set; }
        public string UserId { get; set; }
        public virtual Agent Ag { get; set; }
        public virtual ICollection<InvoiceItem> Items { get; set; }
        
        
    }
}
