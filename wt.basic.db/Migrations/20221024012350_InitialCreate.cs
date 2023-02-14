using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wt.basic.db.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8"),
                    AccountName = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Password = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8"),
                    Telephone = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8"),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.ID);
                },
                comment: "用户表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Sort = table.Column<int>(type: "int(11)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    ModifiedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_tags_tb_users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_tags_tb_users_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ppt标签表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_type",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Sort = table.Column<int>(type: "int(11)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    ModifiedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_type", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_type_tb_users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_type_tb_users_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ppt分类表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_ppt",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    MinPicture = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Download_path = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    TypeID = table.Column<int>(type: "int", nullable: true),
                    Page = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    ModifiedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ppt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_ppt_tb_type_TypeID",
                        column: x => x.TypeID,
                        principalTable: "tb_type",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_ppt_tb_users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_ppt_tb_users_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ppt表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_picture",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Path = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    ModifiedByID = table.Column<int>(type: "int", nullable: true),
                    PptID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_picture", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_picture_tb_ppt_PptID",
                        column: x => x.PptID,
                        principalTable: "tb_ppt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_picture_tb_users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_picture_tb_users_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "轮播图表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_tagppt",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    PPTId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tagppt", x => new { x.PPTId, x.TagId });
                    table.ForeignKey(
                        name: "FK_tb_tagppt_tb_ppt_PPTId",
                        column: x => x.PPTId,
                        principalTable: "tb_ppt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tagppt_tb_tags_TagId",
                        column: x => x.TagId,
                        principalTable: "tb_tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ppt标签表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_userppt_downloads",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PPTId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_userppt_downloads", x => new { x.UserId, x.PPTId });
                    table.ForeignKey(
                        name: "FK_tb_userppt_downloads_tb_ppt_PPTId",
                        column: x => x.PPTId,
                        principalTable: "tb_ppt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_userppt_downloads_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "用户历史下载表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_userppt_favourites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PPTId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_userppt_favourites", x => new { x.UserId, x.PPTId });
                    table.ForeignKey(
                        name: "FK_tb_userppt_favourites_tb_ppt_PPTId",
                        column: x => x.PPTId,
                        principalTable: "tb_ppt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_userppt_favourites_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "用户收藏表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateIndex(
                name: "IX_tb_picture_CreatedByID",
                table: "tb_picture",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_picture_ModifiedByID",
                table: "tb_picture",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_picture_PptID",
                table: "tb_picture",
                column: "PptID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ppt_CreatedByID",
                table: "tb_ppt",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ppt_ModifiedByID",
                table: "tb_ppt",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ppt_TypeID",
                table: "tb_ppt",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tagppt_TagId",
                table: "tb_tagppt",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tags_CreatedByID",
                table: "tb_tags",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tags_ModifiedByID",
                table: "tb_tags",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_type_CreatedByID",
                table: "tb_type",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_type_ModifiedByID",
                table: "tb_type",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_userppt_downloads_PPTId",
                table: "tb_userppt_downloads",
                column: "PPTId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_userppt_favourites_PPTId",
                table: "tb_userppt_favourites",
                column: "PPTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_picture");

            migrationBuilder.DropTable(
                name: "tb_tagppt");

            migrationBuilder.DropTable(
                name: "tb_userppt_downloads");

            migrationBuilder.DropTable(
                name: "tb_userppt_favourites");

            migrationBuilder.DropTable(
                name: "tb_tags");

            migrationBuilder.DropTable(
                name: "tb_ppt");

            migrationBuilder.DropTable(
                name: "tb_type");

            migrationBuilder.DropTable(
                name: "tb_users");
        }
    }
}
