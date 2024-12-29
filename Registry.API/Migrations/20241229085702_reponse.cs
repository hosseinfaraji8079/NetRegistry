using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Registry.API.Migrations
{
    /// <inheritdoc />
    public partial class reponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentLink",
                table: "Registries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PredefinedRejectionReasons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reason = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedRejectionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RejectionReasons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredefinedRejectionReasonId = table.Column<long>(type: "bigint", nullable: false),
                    AdditionalExplanation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RegistryId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectionReasons_PredefinedRejectionReasons_PredefinedRejec~",
                        column: x => x.PredefinedRejectionReasonId,
                        principalTable: "PredefinedRejectionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RejectionReasons_Registries_RegistryId",
                        column: x => x.RegistryId,
                        principalTable: "Registries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RejectionReasons_PredefinedRejectionReasonId",
                table: "RejectionReasons",
                column: "PredefinedRejectionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionReasons_RegistryId",
                table: "RejectionReasons",
                column: "RegistryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RejectionReasons");

            migrationBuilder.DropTable(
                name: "PredefinedRejectionReasons");

            migrationBuilder.DropColumn(
                name: "PaymentLink",
                table: "Registries");
        }
    }
}
