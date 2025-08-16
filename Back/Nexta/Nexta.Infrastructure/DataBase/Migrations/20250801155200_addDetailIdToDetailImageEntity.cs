using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexta.Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class addDetailIdToDetailImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_DetailImages_ImageId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_ImageId",
                table: "Details");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailId",
                table: "DetailImages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DetailImages_DetailId",
                table: "DetailImages",
                column: "DetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailImages_Details_DetailId",
                table: "DetailImages",
                column: "DetailId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailImages_Details_DetailId",
                table: "DetailImages");

            migrationBuilder.DropIndex(
                name: "IX_DetailImages_DetailId",
                table: "DetailImages");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "DetailImages");

            migrationBuilder.CreateIndex(
                name: "IX_Details_ImageId",
                table: "Details",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Details_DetailImages_ImageId",
                table: "Details",
                column: "ImageId",
                principalTable: "DetailImages",
                principalColumn: "Id");
        }
    }
}
