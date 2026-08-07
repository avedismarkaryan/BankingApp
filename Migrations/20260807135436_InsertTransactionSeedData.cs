using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertTransactionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "BalanceAfter", "CreatedAt", "Description", "Type" },
                values: new object[,]
                {
                    { 1, 1, 5000m, 5000m, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maaş", "Deposit" },
                    { 2, 1, 500m, 4500m, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ATM çekim", "Withdrawal" },
                    { 3, 1, 1200m, 3300m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kira", "Withdrawal" },
                    { 4, 2, 8000m, 8000m, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maaş", "Deposit" },
                    { 5, 2, 200m, 7800m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Market", "Withdrawal" },
                    { 6, 2, 3000m, 4800m, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hesap transferi", "Transfer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
