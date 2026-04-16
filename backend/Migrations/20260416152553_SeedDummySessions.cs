using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedDummySessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "sessions",
                columns: new[] { "id", "user_email", "start_datetime", "end_datetime", "stationid" },
                values: new object[,]
                {
                    { 1001, "test@test.com", new DateTime(2026, 4, 10, 8, 15, 0), new DateTime(2026, 4, 10, 9, 0, 0), "station-alpha-01" },
                    { 1002, "test@test.com", new DateTime(2026, 4, 11, 12, 30, 0), new DateTime(2026, 4, 11, 13, 5, 0), "station-beta-07" },
                    { 1003, "driver2@example.com", new DateTime(2026, 4, 12, 7, 45, 0), new DateTime(2026, 4, 12, 8, 20, 0), "station-alpha-01" },
                    { 1004, "driver2@example.com", new DateTime(2026, 4, 13, 18, 10, 0), new DateTime(2026, 4, 13, 19, 0, 0), "station-gamma-03" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "sessions",
                keyColumn: "id",
                keyValues: new object[] { 1001, 1002, 1003, 1004 });
        }
    }
}
