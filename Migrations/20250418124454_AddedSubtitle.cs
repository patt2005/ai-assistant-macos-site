using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIAssistantMacos.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subtitle",
                table: "blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subtitle",
                table: "blogs");
        }
    }
}
