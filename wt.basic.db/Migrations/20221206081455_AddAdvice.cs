using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wt.basic.db.Migrations
{
    public partial class AddAdvice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_advice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Advice = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    type = table.Column<int>(type: "int(1)", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_advice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_advice_tb_users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "advice建议表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateIndex(
                name: "IX_tb_advice_CreatedByID",
                table: "tb_advice",
                column: "CreatedByID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_advice");
        }
    }
}
