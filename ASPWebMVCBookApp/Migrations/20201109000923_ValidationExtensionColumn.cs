using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPWebMVCBookApp.Migrations
{
    public partial class ValidationExtensionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtensionCount",
                table: "borrow",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

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
                keyValue: -5,
                column: "ExtensionCount",
                value: 2);

            migrationBuilder.UpdateData(
                table: "borrow",
                keyColumn: "ID",
                keyValue: -4,
                column: "ExtensionCount",
                value: 4);

            migrationBuilder.UpdateData(
                table: "borrow",
                keyColumn: "ID",
                keyValue: -2,
                column: "ExtensionCount",
                value: 3);

            migrationBuilder.UpdateData(
                table: "borrow",
                keyColumn: "ID",
                keyValue: -1,
                column: "ExtensionCount",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionCount",
                table: "borrow");

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
        }
    }
}
