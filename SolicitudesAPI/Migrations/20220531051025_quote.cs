using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class quote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
