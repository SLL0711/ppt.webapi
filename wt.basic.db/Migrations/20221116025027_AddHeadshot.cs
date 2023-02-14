using Microsoft.EntityFrameworkCore.Migrations;

namespace wt.basic.db.Migrations
{
    public partial class AddHeadshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Headshot",
                table: "tb_users",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headshot",
                table: "tb_users");
        }
    }
}
