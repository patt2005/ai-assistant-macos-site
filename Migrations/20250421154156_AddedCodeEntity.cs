using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIAssistantMacos.Migrations
{
    /// <inheritdoc />
    public partial class AddedCodeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "code-id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "registered-at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "activation-codes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    activatedat = table.Column<DateTime>(name: "activated-at", type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(name: "created-at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activation-codes", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_code-id",
                table: "users",
                column: "code-id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_activation-codes_code-id",
                table: "users",
                column: "code-id",
                principalTable: "activation-codes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_activation-codes_code-id",
                table: "users");

            migrationBuilder.DropTable(
                name: "activation-codes");

            migrationBuilder.DropIndex(
                name: "IX_users_code-id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "code-id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "registered-at",
                table: "users");
        }
    }
}
