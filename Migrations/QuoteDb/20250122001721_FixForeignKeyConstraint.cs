using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Devis_Facture.Migrations.QuoteDb
{
    public partial class FixForeignKeyConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Agents_AgentId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Agents_AgentId",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Ags");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "InvoiceItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InvoiceItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ags",
                table: "Ags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Ags_AgentId",
                table: "Invoice",
                column: "AgentId",
                principalTable: "Ags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Ags_AgentId",
                table: "Quotes",
                column: "AgentId",
                principalTable: "Ags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Ags_AgentId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Ags_AgentId",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ags",
                table: "Ags");

            migrationBuilder.RenameTable(
                name: "Ags",
                newName: "Agents");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "InvoiceItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InvoiceItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Agents_AgentId",
                table: "Invoice",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Agents_AgentId",
                table: "Quotes",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
