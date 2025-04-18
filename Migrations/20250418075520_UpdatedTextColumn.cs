using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIAssistantMacos.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTextColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "test",
                table: "blogs",
                newName: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "blogs",
                newName: "test");
        }
    }
}
