using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "statusRequest",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statusRequest",
                table: "Requests");
        }
    }
}
