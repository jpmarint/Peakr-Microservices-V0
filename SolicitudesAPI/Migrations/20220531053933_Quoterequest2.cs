using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class Quoterequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Requests_RequestId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_RequestId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Quotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Quotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_RequestId",
                table: "Quotes",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Requests_RequestId",
                table: "Quotes",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
