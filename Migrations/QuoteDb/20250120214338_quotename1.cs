using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Devis_Facture.Migrations.QuoteDb
{
    public partial class quotename1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

           

           

            

          


            

           
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "QuoteItems");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Agents");
        
        }
    }
}
