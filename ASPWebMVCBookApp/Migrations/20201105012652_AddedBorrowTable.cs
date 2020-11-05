using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPWebMVCBookApp.Migrations
{
    public partial class AddedBorrowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "borrow",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CheckedOutDate = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "date", nullable: true),
                    BookID = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Borrow_Book",
                        column: x => x.BookID,
                        principalTable: "book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "borrow",
                columns: new[] { "ID", "BookID", "CheckedOutDate", "DueDate", "ReturnedDate" },
                values: new object[,]
                {
                    { -1, -2, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -2, -2, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { -3, -6, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -4, -4, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -5, -1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -6, -6, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "FK_Borrow_Book",
                table: "borrow",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "borrow");

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
