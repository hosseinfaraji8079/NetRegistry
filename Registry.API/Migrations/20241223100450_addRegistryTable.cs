using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Registry.API.Migrations
{
    /// <inheritdoc />
    public partial class addRegistryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Registries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImeI_1 = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    ImeI_2 = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    AcceptTheRules = table.Column<bool>(type: "boolean", nullable: false),
                    Summery = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ForWho = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    TransactionImages = table.Column<List<string>>(type: "text[]", maxLength: 500, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registries");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Users");
        }
    }
}
