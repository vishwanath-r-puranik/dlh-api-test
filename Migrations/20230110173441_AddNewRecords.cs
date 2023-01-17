using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DLHAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DlhModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfExpire = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DlhModel", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DlhModel",
                columns: new[] { "Id", "Address", "DateOfExpire", "DateOfIssue", "Dob", "Name" },
                values: new object[,]
                {
                    { 1, "Washinton Dc", new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1289), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1287), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1286), "Test Name2" },
                    { 3, "Brooks Street", new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1293), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1292), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1291), "Test Name3" },
                    { 11, "York Street, New jersy", new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1284), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1283), new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1251), "Test Name1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DlhModel");
        }
    }
}
