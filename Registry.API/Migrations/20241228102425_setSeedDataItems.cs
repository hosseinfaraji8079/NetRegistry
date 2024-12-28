using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registry.API.Migrations
{
    /// <inheritdoc />
    public partial class setSeedDataItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreateBy", "CreateDate", "IsDelete", "ModifiedDate", "ModifyBy", "ParentId", "SystemName", "Title" },
                values: new object[] { 1L, 0L, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(4401), false, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(3997), 0L, null, "supporter", "supporter" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateBy", "CreateDate", "IsDelete", "ModifiedDate", "ModifyBy", "Title" },
                values: new object[] { 1L, 0L, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(3063), false, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(2839), 0L, "Supporter" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ChatId", "CreateBy", "CreateDate", "FirstName", "IsDelete", "LastName", "ModifiedDate", "ModifyBy", "Password", "UserName" },
                values: new object[] { 1L, 5246606864L, 0L, new DateTime(2024, 12, 28, 10, 24, 24, 342, DateTimeKind.Utc).AddTicks(3658), "Admin", false, "Adminy", new DateTime(2024, 12, 28, 10, 24, 24, 342, DateTimeKind.Utc).AddTicks(2746), 0L, "hossein898989", "Hossein FARAJI" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreateBy", "CreateDate", "IsDelete", "ModifiedDate", "ModifyBy", "PermissionId", "RoleId" },
                values: new object[] { 1L, 0L, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(4933), false, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(4610), 0L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreateBy", "CreateDate", "IsDelete", "ModifiedDate", "ModifyBy", "RoleId", "UserId" },
                values: new object[] { 1L, 0L, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(3588), false, new DateTime(2024, 12, 28, 10, 24, 24, 344, DateTimeKind.Utc).AddTicks(3287), 0L, 1L, 1L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
