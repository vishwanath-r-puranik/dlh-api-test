using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLHAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRecords1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3490), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3488), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3495), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3494), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3492) });

            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3485), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3483), new DateTime(2023, 1, 10, 10, 41, 10, 8, DateTimeKind.Local).AddTicks(3446) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1289), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1287), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1286) });

            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1293), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1292), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "DlhModel",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateOfExpire", "DateOfIssue", "Dob" },
                values: new object[] { new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1284), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1283), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1251) });
        }
    }
}
