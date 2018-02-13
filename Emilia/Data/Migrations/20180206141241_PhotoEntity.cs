using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Emilia.Data.Migrations
{
    public partial class PhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Seller",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Seller",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Product");
        }
    }
}
