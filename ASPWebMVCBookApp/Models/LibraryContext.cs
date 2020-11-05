using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ASPWebMVCBookApp.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection =
                    "server=localhost;" +
                    "port=3306;" +
                    "user=root;" +
                    "database=mvc_library;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection, x => x.ServerVersion(version));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.BirthDate).HasDefaultValue(new DateTime(1890,01,01));
                
                entity.HasData(
                    new Author()
                    {
                        ID = -1,
                        Name = "Mark Twain",
                        BirthDate = new DateTime(1900, 02, 01),
                        DeathDate = new DateTime(1950, 02, 01)
                    },
                    new Author()
                    {
                        ID = -2,
                        Name = "Leo Tolstoy",
                        BirthDate = new DateTime(1900, 02, 01),
                        DeathDate = new DateTime(1955, 02, 01)
                    },
                    new Author()
                    {
                        ID = -3,
                        Name = "Anton Chekov",
                        BirthDate = new DateTime(1900, 02, 01),
                        DeathDate = new DateTime(1970, 02, 01)
                    },
                    new Author()
                    {
                        ID = -4,
                        Name = "Jane Austen",
                        BirthDate = new DateTime(1900, 02, 01),
                        DeathDate = new DateTime(1977, 02, 01)
                    },
                    new Author()
                    {
                        ID = -5,
                        Name = "William Gibson",
                        BirthDate = new DateTime(1900, 02, 01)
                        
                    }
                );
                
            });
            modelBuilder.Entity<Book>(entity =>
            {
                string keyName = "FK_" + nameof(Book) + "_" + nameof(Author);

                // These SHOULD be set automatically. If you want to play around with it by removing these and verify this version of EF works that way, feel free. 
                entity.Property(e => e.Title)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AuthorID)
                .HasName(keyName);

                entity.HasOne(thisEntity => thisEntity.Author)
                .WithMany(parent => parent.Books)
                .HasForeignKey(thisEntity => thisEntity.AuthorID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName(keyName);

                
                entity.HasData
                (
                    new Book()
                    {
                        ID = -1,
                        Title = "The Adventures of Tom Sawyer",
                        AuthorID = -1,
                        //CheckedOutDate = new DateTime(2019, 12, 25),
                        PublicationDate = new DateTime (1876, 01, 01)
                        //DueDate = new DateTime(2020, 01, 01).AddDays(14),
                        //ReturnedDate = new DateTime (2020, 01, 15)
                    },
                    new Book()
                    {
                        ID = -2,
                        Title = "War and Peace",
                        AuthorID = -2,
                       // CheckedOutDate = new DateTime(2020, 02, 01),
                        PublicationDate = new DateTime(1867, 01, 01)
                       // DueDate = new DateTime(2020, 02, 01).AddDays(14),
                       // ReturnedDate = null
                    },
                    new Book()
                    {
                        ID = -3,
                        Title = "Three Sisters",
                        AuthorID = -3,
                        //CheckedOutDate = new DateTime(2020, 03, 01),
                        PublicationDate = new DateTime(1901, 01, 01)
                        //DueDate = new DateTime(2020, 03, 01).AddDays(14).AddDays(14),
                       // ReturnedDate = new DateTime(2020, 03, 29)
                    },
                    new Book()
                    {
                        ID = -4,
                        Title = "Count Zero",
                        AuthorID = -5,
                        //CheckedOutDate = new DateTime(2020, 06, 01),
                        PublicationDate = new DateTime(1986, 01, 01)
                       // DueDate = new DateTime(2020, 06, 01).AddDays(14),
                        //ReturnedDate = new DateTime(2020, 06, 15)
                    },
                    new Book()
                    {
                        ID = -5,
                        Title = "Neuromancer",
                        AuthorID = -5,
                        //CheckedOutDate = new DateTime(2020, 05, 01),
                        PublicationDate = new DateTime(1984, 01, 01)
                        //DueDate = new DateTime(2020, 05, 01).AddDays(14),
                        //ReturnedDate = new DateTime(2020, 05, 15)
                    },
                    new Book()
                    {
                        ID = -6,
                        Title = "Agency",
                        AuthorID = -5,
                        //CheckedOutDate = new DateTime(2020, 07, 01),
                        PublicationDate = new DateTime(2020, 01, 01)
                       // DueDate = new DateTime(2020, 07, 01).AddDays(14),
                       // ReturnedDate = new DateTime(2020, 07, 15)
                    }
                  );
                    
            });

            modelBuilder.Entity<Borrow>(entity =>
            {
                string keyName = "FK_" + nameof(Borrow) + "_" + nameof(Book);

                // These SHOULD be set automatically. If you want to play around with it by removing these and verify this version of EF works that way, feel free. 
           
                entity.HasIndex(e => e.BookID)
                .HasName(keyName);

                entity.HasOne(thisEntity => thisEntity.Book)
                .WithMany(parent => parent.Borrows)
                .HasForeignKey(thisEntity => thisEntity.BookID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(keyName);


                entity.HasData
                (
                    new Borrow()
                    {
                        ID = -1,
                        BookID = -2,
                        CheckedOutDate = new DateTime(2019, 02, 01),
                        DueDate = new DateTime(2019, 01, 01).AddDays(14),
                        ReturnedDate = new DateTime (2019, 01, 15)
                    },
                    new Borrow()
                    {
                        ID = -2,
                        BookID = -2,
                        CheckedOutDate = new DateTime(2020, 02, 01),
                        DueDate = new DateTime(2020, 02, 01).AddDays(14),
                        ReturnedDate = null
                    },
                    new Borrow()
                    {
                        ID = -3,
                        BookID = -6,
                        CheckedOutDate = new DateTime(2020, 03, 01),
                        DueDate = new DateTime(2020, 03, 01).AddDays(14).AddDays(14),
                        ReturnedDate = new DateTime(2020, 03, 29)
                    },
                    new Borrow()
                    {
                        ID = -4,
                        BookID = -4,
                        CheckedOutDate = new DateTime(2020, 06, 01),
                        DueDate = new DateTime(2020, 06, 01).AddDays(14),
                        ReturnedDate = new DateTime(2020, 06, 15)
                    },
                    new Borrow()
                    {
                        ID = -5,
                        BookID = -1,
                        CheckedOutDate = new DateTime(2020, 05, 01),
                        DueDate = new DateTime(2020, 05, 01).AddDays(14),
                        ReturnedDate = new DateTime(2020, 05, 15)
                    },
                    new Borrow()
                    {
                        ID = -6,               
                        BookID = -6,
                        CheckedOutDate = new DateTime(2020, 07, 01),
                        DueDate = new DateTime(2020, 07, 01).AddDays(14),
                        ReturnedDate = new DateTime(2020, 07, 15)
                    }
                  );

            });



        }
    }
}
