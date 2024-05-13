using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubtelWallet.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Customers");
        }
    }
}
