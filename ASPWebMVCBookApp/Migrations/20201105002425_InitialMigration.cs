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
                    Name = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(1890, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DeathDate = table.Column<DateTime>(type: "date", nullable: false)
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
                    Title = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    PublicationDate = table.Column<DateTime>(type: "date", nullable: false),
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
                columns: new[] { "ID", "BirthDate", "DeathDate", "Name" },
                values: new object[,]
                {
                    { -1, new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1950, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark Twain" },
                    { -2, new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1955, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leo Tolstoy" },
                    { -3, new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anton Chekov" },
                    { -4, new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1977, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Austen" },
                    { -5, new DateTime(1900, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William Gibson" }
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "ID", "AuthorID", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { -1, -1, new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Adventures of Tom Sawyer" },
                    { -2, -2, new DateTime(1867, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "War and Peace" },
                    { -3, -3, new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Three Sisters" },
                    { -4, -5, new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Count Zero" },
                    { -5, -5, new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neuromancer" },
                    { -6, -5, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agency" }
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
