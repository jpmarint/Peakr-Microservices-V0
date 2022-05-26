using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SolicitudesAPI.Models
{
    public partial class SolicitudesAPIContext : DbContext
    {
        public SolicitudesAPIContext()
        {
        }

        public SolicitudesAPIContext(DbContextOptions<SolicitudesAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adress> Adresses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SolicitudesAPI;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adress>(entity =>
            {
                entity.ToTable("Adress");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Line1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Line2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyDescription).HasMaxLength(200);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogoPath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nit).HasColumnName("NIT");

                entity.Property(e => e.WebSiteUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.Property(e => e.NetCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuoteExpirationDate).HasColumnType("date");

                entity.Property(e => e.QuoteNotes).HasMaxLength(500);

                entity.Property(e => e.QuoteProductName).HasMaxLength(300);

                entity.Property(e => e.SellerIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ServiceCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TaxHold).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Taxes).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalGrossPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_Quotes_RequestId");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.PaymentConditions).HasMaxLength(200);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.QuerySearch).HasMaxLength(200);

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.RequestNotes).HasMaxLength(300);

                entity.Property(e => e.RequestStatus).HasMaxLength(20);

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.AdressId)
                    .HasConstraintName("FK_Request_AdressId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Request_CompanyId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
