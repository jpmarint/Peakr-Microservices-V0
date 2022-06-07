using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    public partial class QuoteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.CreateTable(
            //    name: "Quotes",
            //    columns: table => new
            //    {
            //        QuoteId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuoteProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DeliveryDeadLineInDays = table.Column<int>(type: "int", nullable: false),
            //        QuoteExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        TotalGrossPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        IVA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        TotalIVA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        NetCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        TaxWithholding = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        ServiceCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        SellerIncome = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //        NotesToClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsProductExactMatch = table.Column<bool>(type: "bit", nullable: false),
            //        CompanyId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Quotes", x => x.QuoteId);
            //        table.ForeignKey(
            //            name: "FK_Quotes_Companies_CompanyId",
            //            column: x => x.CompanyId,
            //            principalTable: "Companies",
            //            principalColumn: "CompanyId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Requests",
            //    columns: table => new
            //    {
            //        RequestId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuerySearch = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsExactProduct = table.Column<bool>(type: "bit", nullable: false),
            //        RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        PaymentConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Quantity = table.Column<int>(type: "int", nullable: false),
            //        ProductNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ChosenQuote = table.Column<int>(type: "int", nullable: true),
            //        CompanyId = table.Column<int>(type: "int", nullable: false),
            //        StatusRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AddressId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Requests", x => x.RequestId);
            //        table.ForeignKey(
            //            name: "FK_Requests_Address_AddressId",
            //            column: x => x.AddressId,
            //            principalTable: "Address",
            //            principalColumn: "AddressId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Requests_Companies_CompanyId",
            //            column: x => x.CompanyId,
            //            principalTable: "Companies",
            //            principalColumn: "CompanyId",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuoteRequest",
            //    columns: table => new
            //    {
            //        QuoteId = table.Column<int>(type: "int", nullable: false),
            //        RequestId = table.Column<int>(type: "int", nullable: false),
            //        IsPurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
            //        IsCancelled = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuoteRequest", x => new { x.QuoteId, x.RequestId });
            //        table.ForeignKey(
            //            name: "FK_QuoteRequest_Quotes_QuoteId",
            //            column: x => x.QuoteId,
            //            principalTable: "Quotes",
            //            principalColumn: "QuoteId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_QuoteRequest_Requests_RequestId",
            //            column: x => x.RequestId,
            //            principalTable: "Requests",
            //            principalColumn: "RequestId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuoteRequest_RequestId",
            //    table: "QuoteRequest",
            //    column: "RequestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Quotes_CompanyId",
            //    table: "Quotes",
            //    column: "CompanyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Requests_AddressId",
            //    table: "Requests",
            //    column: "AddressId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Requests_CompanyId",
            //    table: "Requests",
            //    column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "QuoteRequest");

            //migrationBuilder.DropTable(
            //    name: "Quotes");

            //migrationBuilder.DropTable(
            //    name: "Requests");

            //migrationBuilder.DropTable(
            //    name: "Companies");

            //migrationBuilder.DropTable(
            //    name: "Address");
        }
    }
}
