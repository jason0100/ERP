using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apple",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apple", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partner",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organization_id = table.Column<int>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    landline = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    tax_ID_number = table.Column<int>(nullable: true),
                    contact_number = table.Column<string>(nullable: true),
                    contact_person = table.Column<string>(nullable: true),
                    capital = table.Column<decimal>(nullable: true),
                    representative = table.Column<string>(nullable: true),
                    website = table.Column<string>(nullable: true),
                    status = table.Column<byte>(nullable: true),
                    created = table.Column<DateTime>(nullable: true),
                    updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    budget = table.Column<decimal>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: true),
                    date_end = table.Column<DateTime>(nullable: true),
                    internal_HR = table.Column<int>(nullable: true),
                    outsourcing_HR = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    status = table.Column<byte>(nullable: true),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applePens",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<int>(nullable: false),
                    appleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applePens", x => x.id);
                    table.ForeignKey(
                        name: "FK_applePens_apple_appleId",
                        column: x => x.appleId,
                        principalTable: "apple",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expenditure",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_id = table.Column<int>(nullable: true),
                    item_id = table.Column<int>(nullable: true),
                    due_date = table.Column<DateTime>(nullable: false),
                    payment_date = table.Column<DateTime>(nullable: true),
                    amount_payable = table.Column<decimal>(nullable: false),
                    amount_paid = table.Column<decimal>(nullable: true),
                    status = table.Column<byte>(nullable: true),
                    partner_id = table.Column<int>(nullable: true),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenditure", x => x.id);
                    table.ForeignKey(
                        name: "FK_expenditure_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expenditure_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "project_partner",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partner_id = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_partner", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_partner_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_partner_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applePens_appleId",
                table: "applePens",
                column: "appleId");

            migrationBuilder.CreateIndex(
                name: "IX_expenditure_partner_id",
                table: "expenditure",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_expenditure_project_id",
                table: "expenditure",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_partner_partner_id",
                table: "project_partner",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_partner_project_id",
                table: "project_partner",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applePens");

            migrationBuilder.DropTable(
                name: "expenditure");

            migrationBuilder.DropTable(
                name: "project_partner");

            migrationBuilder.DropTable(
                name: "apple");

            migrationBuilder.DropTable(
                name: "partner");

            migrationBuilder.DropTable(
                name: "project");
        }
    }
}
