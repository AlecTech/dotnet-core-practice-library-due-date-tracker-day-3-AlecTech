using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPWebMVCBookApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    PublicationDate = table.Column<DateTime>(type: "date", nullable: false),
                    CheckedOutDate = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "date", nullable: true),
                    AuthorID = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_Author",
                        column: x => x.AuthorID,
                        principalTable: "author",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "author",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { -1, "Mark Twain" },
                    { -2, "Leo Tolstoy" },
                    { -3, "Anton Chekov" },
                    { -4, "Jane Austen" },
                    { -5, "William Gibson" }
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "ID", "AuthorID", "CheckedOutDate", "DueDate", "PublicationDate", "ReturnedDate", "Title" },
                values: new object[,]
                {
                    { -1, -1, new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Adventures of Tom Sawyer" },
                    { -2, -2, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1867, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "War and Peace" },
                    { -3, -3, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Three Sisters" },
                    { -4, -5, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Count Zero" },
                    { -5, -5, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neuromancer" },
                    { -6, -5, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agency" }
                });

            migrationBuilder.CreateIndex(
                name: "FK_Book_Author",
                table: "book",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "author");
        }
    }
}
