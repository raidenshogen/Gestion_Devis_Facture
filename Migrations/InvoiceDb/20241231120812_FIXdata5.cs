using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Devis_Facture.Migrations.InvoiceDb
{
    public partial class FIXdata6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

         

            

           

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "InvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_AgentId",
                table: "Invoice",
                newName: "IX_Invoice_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Ags_AgentId",
                table: "Invoice",
                column: "AgentId",
                principalTable: "Ags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

           
        


          

           

          
           

          

          

           

         

            
        }
    }
}
