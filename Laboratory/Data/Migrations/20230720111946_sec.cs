using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory.Data.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyRequestLimitation",
                table: "Manage",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Manage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Manage");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Manage",
                newName: "DailyRequestLimitation");
        }
    }
}
