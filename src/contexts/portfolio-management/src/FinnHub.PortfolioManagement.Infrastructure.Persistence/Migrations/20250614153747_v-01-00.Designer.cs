﻿// <auto-generated />
using System;
using FinnHub.PortfolioManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinnHub.PortfolioManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250614153747_v-01-00")]
    partial class v0100
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AssetSymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("asset_symbol");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid")
                        .HasColumnName("portfolio_id");

                    b.Property<int>("Quantity")
                        .HasMaxLength(10)
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_positions");

                    b.HasIndex("PortfolioId")
                        .HasDatabaseName("ix_positions_portfolio_id");

                    b.ToTable("positions", (string)null);
                });

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AssetSymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("asset_symbol");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("boolean")
                        .HasColumnName("is_settled");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid")
                        .HasColumnName("portfolio_id");

                    b.Property<int>("Quantity")
                        .HasMaxLength(10)
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<DateTimeOffset>("TransactionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("PortfolioId")
                        .HasDatabaseName("ix_transactions_portfolio_id");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_portfolios");

                    b.HasIndex("UserId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_portfolios_user_id_name");

                    b.ToTable("portfolios", (string)null);
                });

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Entities.Position", b =>
                {
                    b.HasOne("FinnHub.PortfolioManagement.Domain.Aggregates.Portfolio", null)
                        .WithMany("Positions")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_positions_portfolios_portfolio_id");

                    b.OwnsOne("FinnHub.PortfolioManagement.Domain.Aggregates.ValueObjects.Money", "AverageCost", b1 =>
                        {
                            b1.Property<Guid>("PositionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("average_cost_currency");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)")
                                .HasColumnName("average_cost_value");

                            b1.HasKey("PositionId");

                            b1.ToTable("positions");

                            b1.WithOwner()
                                .HasForeignKey("PositionId")
                                .HasConstraintName("fk_positions_positions_id");
                        });

                    b.OwnsOne("FinnHub.PortfolioManagement.Domain.Aggregates.ValueObjects.Money", "CurrentMarketPrice", b1 =>
                        {
                            b1.Property<Guid>("PositionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("current_market_price_currency");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)")
                                .HasColumnName("current_market_price_value");

                            b1.HasKey("PositionId");

                            b1.ToTable("positions");

                            b1.WithOwner()
                                .HasForeignKey("PositionId")
                                .HasConstraintName("fk_positions_positions_id");
                        });

                    b.Navigation("AverageCost")
                        .IsRequired();

                    b.Navigation("CurrentMarketPrice");
                });

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Entities.Transaction", b =>
                {
                    b.HasOne("FinnHub.PortfolioManagement.Domain.Aggregates.Portfolio", null)
                        .WithMany("Transactions")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_portfolios_portfolio_id");

                    b.OwnsOne("FinnHub.PortfolioManagement.Domain.Aggregates.ValueObjects.Money", "CurrentMarketValue", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("current_market_value_currency");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)")
                                .HasColumnName("current_market_value_value");

                            b1.HasKey("TransactionId");

                            b1.ToTable("transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId")
                                .HasConstraintName("fk_transactions_transactions_id");
                        });

                    b.OwnsOne("FinnHub.PortfolioManagement.Domain.Aggregates.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("price_currency");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)")
                                .HasColumnName("price_value");

                            b1.HasKey("TransactionId");

                            b1.ToTable("transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId")
                                .HasConstraintName("fk_transactions_transactions_id");
                        });

                    b.Navigation("CurrentMarketValue");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("FinnHub.PortfolioManagement.Domain.Aggregates.Portfolio", b =>
                {
                    b.Navigation("Positions");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
