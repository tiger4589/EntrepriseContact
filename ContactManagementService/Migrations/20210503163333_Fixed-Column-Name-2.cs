using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManagementService.Migrations
{
    public partial class FixedColumnName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntrepriseContacts_Entreprises_EnterpriseId",
                table: "EntrepriseContacts");

            migrationBuilder.RenameColumn(
                name: "EnterpriseId",
                table: "EntrepriseContacts",
                newName: "EntrepriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseContacts_Entreprises_EntrepriseId",
                table: "EntrepriseContacts",
                column: "EntrepriseId",
                principalTable: "Entreprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntrepriseContacts_Entreprises_EntrepriseId",
                table: "EntrepriseContacts");

            migrationBuilder.RenameColumn(
                name: "EntrepriseId",
                table: "EntrepriseContacts",
                newName: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseContacts_Entreprises_EnterpriseId",
                table: "EntrepriseContacts",
                column: "EnterpriseId",
                principalTable: "Entreprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
