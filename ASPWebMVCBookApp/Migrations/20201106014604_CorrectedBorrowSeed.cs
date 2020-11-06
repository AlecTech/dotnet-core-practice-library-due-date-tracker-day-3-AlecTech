using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPWebMVCBookApp.Migrations
{
    public partial class CorrectedBorrowSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -5,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -4,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -3,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -2,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -1,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "borrow",
                keyColumn: "ID",
                keyValue: -1,
                columns: new[] { "CheckedOutDate", "DueDate", "ReturnedDate" },
                values: new object[] { new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -5,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -4,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -3,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -2,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "author",
                keyColumn: "ID",
                keyValue: -1,
                column: "BirthDate",
                value: new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "borrow",
                keyColumn: "ID",
                keyValue: -1,
                columns: new[] { "CheckedOutDate", "DueDate", "ReturnedDate" },
                values: new object[] { new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
