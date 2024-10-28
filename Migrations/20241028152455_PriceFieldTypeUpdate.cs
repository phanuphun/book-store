using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookStoreManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class PriceFieldTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Books",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "translater",
                table: "Books",
                newName: "Translater");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "thickness",
                table: "Books",
                newName: "Thickness");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Books",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Books",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "pages",
                table: "Books",
                newName: "Pages");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Books",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "drawer",
                table: "Books",
                newName: "Drawer");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Books",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Books",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Pages",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Books",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Translater",
                table: "Books",
                newName: "translater");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Thickness",
                table: "Books",
                newName: "thickness");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Books",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Books",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Pages",
                table: "Books",
                newName: "pages");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Drawer",
                table: "Books",
                newName: "drawer");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "author");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Books",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "weight",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "pages",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
