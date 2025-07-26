using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexta.Infrastructure.DataBase
{
    /// <inheritdoc />
    public partial class ChangeBase64StringFieldInNewsImageAndDetailImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Base64string",
                table: "NewsImages",
                newName: "Base64String");

            migrationBuilder.RenameColumn(
                name: "Base64string",
                table: "DetailImages",
                newName: "Base64String");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Base64String",
                table: "NewsImages",
                newName: "Base64string");

            migrationBuilder.RenameColumn(
                name: "Base64String",
                table: "DetailImages",
                newName: "Base64string");
        }
    }
}
