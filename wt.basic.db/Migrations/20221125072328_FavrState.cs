using Microsoft.EntityFrameworkCore.Migrations;

namespace wt.basic.db.Migrations
{
    public partial class FavrState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FavrState",
                table: "tb_ppt",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavrState",
                table: "tb_ppt");
        }
    }
}
