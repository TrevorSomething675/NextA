using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexta.Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailFieldImageIdAndImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bucket",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Details",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Details_ImageId",
                table: "Details",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Images_ImageId",
                table: "Details",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Images_ImageId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_ImageId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Bucket",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Details");
        }
    }
}
