using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubtelWallet.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Wallets_AccountNumber",
                table: "Wallets",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_MaskedVisaNumber",
                table: "Wallets",
                column: "MaskedVisaNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_AccountNumber",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_MaskedVisaNumber",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers");
        }
    }
}
