using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookStoreManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addWeightToBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "weight",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "weight",
                table: "Books");
        }
    }
}
