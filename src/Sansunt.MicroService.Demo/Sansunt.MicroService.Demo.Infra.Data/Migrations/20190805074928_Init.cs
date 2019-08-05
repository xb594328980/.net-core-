using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sansunt.MicroService.Demo.Infra.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    account = table.Column<string>(type: "varchar(64)", nullable: false),
                    nick_name = table.Column<string>(type: "varchar(64)", nullable: false),
                    pwd = table.Column<string>(type: "varchar(64)", nullable: false),
                    status = table.Column<int>(nullable: false),
                    create_by = table.Column<string>(type: "varchar(64)", nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_by = table.Column<string>(type: "varchar(64)", nullable: true),
                    update_time = table.Column<DateTime>(nullable: true),
                    del_flag = table.Column<int>(nullable: false),
                    remark = table.Column<string>(type: "varchar(512)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_staff", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_staff_account_del_flag_Id",
                table: "sys_staff",
                columns: new[] { "account", "del_flag", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_staff");
        }
    }
}
