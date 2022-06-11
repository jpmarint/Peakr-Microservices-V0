using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class Cambiosdocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Requests",
                newName: "FileKey");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Quotes",
                newName: "FileKey");

            migrationBuilder.RenameColumn(
                name: "RutDocPath",
                table: "Companies",
                newName: "RutDocKey");

            migrationBuilder.RenameColumn(
                name: "LogoPath",
                table: "Companies",
                newName: "RutDocGuid");

            migrationBuilder.RenameColumn(
                name: "LegalExistenceDocPath",
                table: "Companies",
                newName: "LogoKey");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Companies",
                newName: "LogoGuid");

            migrationBuilder.RenameColumn(
                name: "BankAccountDocPath",
                table: "Companies",
                newName: "LegalExistenceDocKey");

            migrationBuilder.AddColumn<string>(
                name: "FileGuid",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileGuid",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountDocGuid",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountDocKey",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageGuid",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageKey",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalExistenceDocGuid",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileGuid",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "FileGuid",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "BankAccountDocGuid",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BankAccountDocKey",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImageGuid",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImageKey",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LegalExistenceDocGuid",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "FileKey",
                table: "Requests",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "FileKey",
                table: "Quotes",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "RutDocKey",
                table: "Companies",
                newName: "RutDocPath");

            migrationBuilder.RenameColumn(
                name: "RutDocGuid",
                table: "Companies",
                newName: "LogoPath");

            migrationBuilder.RenameColumn(
                name: "LogoKey",
                table: "Companies",
                newName: "LegalExistenceDocPath");

            migrationBuilder.RenameColumn(
                name: "LogoGuid",
                table: "Companies",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "LegalExistenceDocKey",
                table: "Companies",
                newName: "BankAccountDocPath");

        }
    }
}
