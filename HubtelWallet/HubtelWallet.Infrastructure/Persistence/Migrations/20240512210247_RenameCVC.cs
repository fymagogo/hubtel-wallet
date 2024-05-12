using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubtelWallet.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameCVC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CVC",
                table: "Wallets",
                newName: "Cvc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cvc",
                table: "Wallets",
                newName: "CVC");
        }
    }
}
