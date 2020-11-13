using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applePens_apple_appleId",
                table: "applePens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_apple",
                table: "apple");

            migrationBuilder.RenameTable(
                name: "apple",
                newName: "apples");

            migrationBuilder.AddPrimaryKey(
                name: "PK_apples",
                table: "apples",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applePens_apples_appleId",
                table: "applePens",
                column: "appleId",
                principalTable: "apples",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applePens_apples_appleId",
                table: "applePens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_apples",
                table: "apples");

            migrationBuilder.RenameTable(
                name: "apples",
                newName: "apple");

            migrationBuilder.AddPrimaryKey(
                name: "PK_apple",
                table: "apple",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applePens_apple_appleId",
                table: "applePens",
                column: "appleId",
                principalTable: "apple",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
