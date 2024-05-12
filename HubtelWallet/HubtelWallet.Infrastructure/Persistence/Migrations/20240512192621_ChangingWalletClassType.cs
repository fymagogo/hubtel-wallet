using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubtelWallet.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangingWalletClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Wallets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CVC",
                table: "Wallets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Wallets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Wallets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaskedVisaNumber",
                table: "Wallets",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVC",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MaskedVisaNumber",
                table: "Wallets");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Wallets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
