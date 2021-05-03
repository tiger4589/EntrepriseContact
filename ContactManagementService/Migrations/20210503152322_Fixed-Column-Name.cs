using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManagementService.Migrations
{
    public partial class FixedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntrepriseAddresses_Entreprises_EntrepirseId",
                table: "EntrepriseAddresses");

            migrationBuilder.RenameColumn(
                name: "EntrepirseId",
                table: "EntrepriseAddresses",
                newName: "EntrepriseId");

            migrationBuilder.RenameIndex(
                name: "IX_EntrepriseAddresses_EntrepirseId",
                table: "EntrepriseAddresses",
                newName: "IX_EntrepriseAddresses_EntrepriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseAddresses_Entreprises_EntrepriseId",
                table: "EntrepriseAddresses",
                column: "EntrepriseId",
                principalTable: "Entreprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntrepriseAddresses_Entreprises_EntrepriseId",
                table: "EntrepriseAddresses");

            migrationBuilder.RenameColumn(
                name: "EntrepriseId",
                table: "EntrepriseAddresses",
                newName: "EntrepirseId");

            migrationBuilder.RenameIndex(
                name: "IX_EntrepriseAddresses_EntrepriseId",
                table: "EntrepriseAddresses",
                newName: "IX_EntrepriseAddresses_EntrepirseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseAddresses_Entreprises_EntrepirseId",
                table: "EntrepriseAddresses",
                column: "EntrepirseId",
                principalTable: "Entreprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
