﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolicitudesAPI;

#nullable disable

namespace SolicitudesAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SolicitudesAPI.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<int>("CategoryCode")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("BankAccountDocPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CompanyType")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EstablishedSince")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LegalExistenceDocPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeakrContractDocPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutDocPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalEmployees")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebSiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearlySalesVolume")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.CompanyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyCategories");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuoteId"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("DeliveryDeadLineInDays")
                        .HasColumnType("int");

                    b.Property<decimal>("IVA")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsProductExactMatch")
                        .HasColumnType("bit");

                    b.Property<decimal>("NetCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NotesToClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerUnit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("QuoteExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuoteProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SellerIncome")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ServiceCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxWithholding")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalGrossPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalIVA")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("QuoteId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.QuoteRequest", b =>
                {
                    b.Property<int>("QuoteId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPurchaseOrder")
                        .HasColumnType("bit");

                    b.HasKey("QuoteId", "RequestId");

                    b.HasIndex("RequestId");

                    b.ToTable("QuoteRequest");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("ChosenQuote")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExactProduct")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentConditions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductNeeds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("QuerySearch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deliveryInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.RequestCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestCategories");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Company", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Address", "Address")
                        .WithMany("Companies")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Address");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.CompanyCategory", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Category", "Category")
                        .WithMany("companyCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SolicitudesAPI.Models.Company", "Company")
                        .WithMany("companyCategories")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Quote", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Company", "Company")
                        .WithMany("Quotes")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.QuoteRequest", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Quote", "Quote")
                        .WithMany("QuoteRequests")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SolicitudesAPI.Models.Request", "Request")
                        .WithMany("QuoteRequest")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Quote");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Request", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Address", "Address")
                        .WithMany("Requests")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SolicitudesAPI.Models.Company", "Companies")
                        .WithMany("requests")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Companies");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.RequestCategory", b =>
                {
                    b.HasOne("SolicitudesAPI.Models.Category", "Category")
                        .WithMany("requestCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SolicitudesAPI.Models.Request", "Request")
                        .WithMany("requestCategories")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Address", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Category", b =>
                {
                    b.Navigation("companyCategories");

                    b.Navigation("requestCategories");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Company", b =>
                {
                    b.Navigation("Quotes");

                    b.Navigation("companyCategories");

                    b.Navigation("requests");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Quote", b =>
                {
                    b.Navigation("QuoteRequests");
                });

            modelBuilder.Entity("SolicitudesAPI.Models.Request", b =>
                {
                    b.Navigation("QuoteRequest");

                    b.Navigation("requestCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
