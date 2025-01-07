using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registry.API.Migrations
{
    /// <inheritdoc />
    public partial class customs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomsAmount",
                table: "Registries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomsIBAN",
                table: "Registries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomsPaymentId",
                table: "Registries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelerBirthDate",
                table: "Registries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TravelerNationalId",
                table: "Registries",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TravelerPhone",
                table: "Registries",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomsAmount",
                table: "Registries");

            migrationBuilder.DropColumn(
                name: "CustomsIBAN",
                table: "Registries");

            migrationBuilder.DropColumn(
                name: "CustomsPaymentId",
                table: "Registries");

            migrationBuilder.DropColumn(
                name: "TravelerBirthDate",
                table: "Registries");

            migrationBuilder.DropColumn(
                name: "TravelerNationalId",
                table: "Registries");

            migrationBuilder.DropColumn(
                name: "TravelerPhone",
                table: "Registries");
        }
    }
}
