using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexta.Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedDateToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedDate",
                table: "Orders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Orders");
        }
    }
}
