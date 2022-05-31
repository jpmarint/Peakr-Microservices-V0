using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class Request1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "deliveryInstructions",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);


        }
    }
}
