using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Devis_Facture.Migrations
{
    public partial class fourMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7403bfad-3361-49e1-8d8a-7980f8aff202", "e8a198fb-a4e8-4c3c-946e-a06d520dd1b5", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d450551-7175-4050-a9c5-a3de74b131bb", "b5d66bc5-f009-48d9-8c2b-4e2cd03fc4c0", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7403bfad-3361-49e1-8d8a-7980f8aff202");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d450551-7175-4050-a9c5-a3de74b131bb");
        }
    }
}
