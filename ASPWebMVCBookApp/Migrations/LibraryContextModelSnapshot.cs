﻿// <auto-generated />
using System;
using ASPWebMVCBookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASPWebMVCBookApp.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ASPWebMVCBookApp.Models.Author", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ID");

                    b.ToTable("author");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            Name = "Mark Twain"
                        },
                        new
                        {
                            ID = -2,
                            Name = "Leo Tolstoy"
                        },
                        new
                        {
                            ID = -3,
                            Name = "Anton Chekov"
                        },
                        new
                        {
                            ID = -4,
                            Name = "Jane Austen"
                        },
                        new
                        {
                            ID = -5,
                            Name = "William Gibson"
                        });
                });

            modelBuilder.Entity("ASPWebMVCBookApp.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int(10)");

                    b.Property<int>("AuthorID")
                        .HasColumnName("AuthorID")
                        .HasColumnType("int(10)");

                    b.Property<DateTime>("CheckedOutDate")
                        .HasColumnName("CheckedOutDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("DueDate")
                        .HasColumnName("DueDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnName("PublicationDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnName("ReturnedDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID")
                        .HasName("FK_Book_Author");

                    b.ToTable("book");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            AuthorID = -1,
                            CheckedOutDate = new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnedDate = new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Adventures of Tom Sawyer"
                        },
                        new
                        {
                            ID = -2,
                            AuthorID = -2,
                            CheckedOutDate = new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(1867, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "War and Peace"
                        },
                        new
                        {
                            ID = -3,
                            AuthorID = -3,
                            CheckedOutDate = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnedDate = new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Three Sisters"
                        },
                        new
                        {
                            ID = -4,
                            AuthorID = -5,
                            CheckedOutDate = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnedDate = new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Count Zero"
                        },
                        new
                        {
                            ID = -5,
                            AuthorID = -5,
                            CheckedOutDate = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnedDate = new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Neuromancer"
                        },
                        new
                        {
                            ID = -6,
                            AuthorID = -5,
                            CheckedOutDate = new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PublicationDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnedDate = new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Agency"
                        });
                });

            modelBuilder.Entity("ASPWebMVCBookApp.Models.Book", b =>
                {
                    b.HasOne("ASPWebMVCBookApp.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorID")
                        .HasConstraintName("FK_Book_Author")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
