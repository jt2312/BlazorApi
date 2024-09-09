using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopOnlineApi.Migrations
{
    public partial class AddProductCategoryIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconCSS",
                table: "productCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconCSS",
                value: "fas fa-couch");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconCSS",
                value: "fas fa-head");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconCSS",
                value: "fas fa-headphones");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "IconCSS",
                value: "fas fa-shoes-sprints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconCSS",
                table: "productCategories");
        }
    }
}
