namespace Gestion_Devis_Facture.Model
{
    public class InvoiceTemplateSettings
    {
        public int Id { get; set; }
        public int InvoiceTemplateId { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string FontFamily { get; set; }
        public string LogoUrl { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowSignature { get; set; }
        public string CustomHeader { get; set; }
        public string CustomFooter { get; set; }
        public string DateFormat { get; set; }
        public string CurrencyFormat { get; set; }
    }
}
