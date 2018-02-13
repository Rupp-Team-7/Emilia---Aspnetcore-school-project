using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Emilia.Data.Migrations
{
    public partial class FinalEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Material",
                table: "ProductDetail",
                newName: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "ProductDetail",
                newName: "Material");
        }
    }
}
