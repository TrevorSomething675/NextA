using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexta.Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class addCountInUserDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "UserDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "UserDetails");
        }
    }
}
