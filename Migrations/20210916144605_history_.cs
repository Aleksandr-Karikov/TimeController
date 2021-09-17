using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeController.Migrations
{
    public partial class history_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => new { x.UserID, x.Date });
                    table.ForeignKey(
                        name: "FK_history_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.AddColumn<string>(
                name: "Friday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Monday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Saturday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sunday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thursday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tuesday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Wednesday",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
