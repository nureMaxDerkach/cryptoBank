using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CryptoBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    RateToUsd = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Ukraine" },
                    { 2L, "Germany" },
                    { 3L, "United States" },
                    { 4L, "United Kingdom" },
                    { 5L, "Canada" },
                    { 6L, "Singapore" },
                    { 7L, "Switzerland" },
                    { 8L, "United Arab Emirates" },
                    { 9L, "Netherlands" },
                    { 10L, "Estonia" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name", "Type" },
                values: new object[,]
                {
                    { 1L, "USD", "US Dollar", (byte)1 },
                    { 2L, "EUR", "Euro", (byte)1 },
                    { 3L, "UAH", "Ukrainian hryvnia", (byte)1 },
                    { 4L, "BTC", "Bitcoin", (byte)2 },
                    { 5L, "ETH", "Ethereum", (byte)2 },
                    { 6L, "TRX", "Tronix", (byte)2 },
                    { 7L, "BNB", "Build and Build", (byte)2 },
                    { 8L, "USDT", "Tether USD", (byte)2 },
                    { 9L, "USDC", "USD Coin", (byte)2 }
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CurrencyId", "RateToUsd", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 2L, 1.16m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, 3L, 0.023m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, 4L, 62229m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, 5L, 1670m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, 6L, 0.33m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, 7L, 592.20m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, 8L, 1m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, 9L, 1m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");
        }
    }
}
