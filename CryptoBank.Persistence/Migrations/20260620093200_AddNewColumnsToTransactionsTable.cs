using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RecipientReference",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecipientReference",
                table: "Transactions");
        }
    }
}
