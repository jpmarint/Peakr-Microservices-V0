using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class categories3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestCategories",
                table: "RequestCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RequestCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompanyCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestCategories",
                table: "RequestCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCategories_RequestId",
                table: "RequestCategories",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategories_CompanyId",
                table: "CompanyCategories",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestCategories",
                table: "RequestCategories");

            migrationBuilder.DropIndex(
                name: "IX_RequestCategories_RequestId",
                table: "RequestCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCategories_CompanyId",
                table: "CompanyCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequestCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompanyCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestCategories",
                table: "RequestCategories",
                columns: new[] { "RequestId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories",
                columns: new[] { "CompanyId", "CategoryId" });
        }
    }
}
