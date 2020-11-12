﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASPWebMVCBookApp.Models
{
    [Table("book")]
    public class Book
    {
        public string GetCheckOutDate
        {
            get { 
                var checkoutdate = "book has never been checkedout";
                if(Borrows.LastOrDefault() !=null)
                {
                    checkoutdate = Borrows.LastOrDefault().CheckedOutDate.ToLongDateString();
                }
                return checkoutdate;
            }
        }
        public string GetDueDate
        {
            get
            {
                var duedate = "book is not due yet";
                if (Borrows.LastOrDefault() != null)
                {
                    duedate = Borrows.LastOrDefault().DueDate.ToLongDateString();
                }
                return duedate;
            }
        }
        public string GetReturnedDate
        {
            get
            {
                var returneddate = "never borrowed";
                if (Borrows.LastOrDefault() != null && Borrows.LastOrDefault().ReturnedDate.HasValue)
                {
                    returneddate = Borrows.LastOrDefault().ReturnedDate.Value.ToLongDateString();
                }
                else if(Borrows.LastOrDefault() != null )
                {
                    returneddate = "Still borrowed";
                }
                return returneddate;
            }
        }

        public string GetExtensions
        {
            get
            {
                var extensions = "book has no extensions left";
                if (Borrows.LastOrDefault() != null)
                {
                    extensions = Borrows.LastOrDefault().ExtensionCount.ToString();
                }
                return extensions;
            }
        }
        public Book()
        {
            Borrows = new HashSet<Borrow>();
        }

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column("Title", TypeName = "varchar(100)")]
        public string Title { get; set; }

        [Required]
        [Column("PublicationDate", TypeName = "date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [Column("AuthorID", TypeName = "int(10)")]
        public int AuthorID { get; set; }

        // This attribute specifies which database field is the foreign key. Typically in the child (many side of the 1-many).
        [ForeignKey(nameof(AuthorID))]

        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Author.Books))]
        public virtual Author Author { get; set; }

        [InverseProperty(nameof(Models.Borrow.Book))]
        public virtual ICollection<Borrow> Borrows { get; set; }

        /*
        //id Title Author and PublicationDate have only getters not Setters allowed, because we can not change them

        //intelesence suggested to add those private initializers, i suppose because they are inside the scope and private access modifier is not visible
        private int _id;
        private string _title;
        private string _author;
        public int ID 
            {
                get
                {
                    return _id;
                }
                private set
                {
                    _id = value;
                }
            }

        public string Title
        {
            get
            {
                return _title;
            }
            private set
            {
                _title = value;
            }
        }

        public string Author
        {
            get
            {
                return _author;
            }
            private set
            {
                _author = value;
            }
        }
 
        private DateTime _publicationDate;
        public DateTime PublicationDate
        {
            get
            {
                return _publicationDate;
            }
            private set
            {
                _publicationDate = value;
            }
        }
        // these are getter and setter properties
        public DateTime CheckedOutDate { get; set; }
        public DateTime DueDate { get; set; }
        // question mark indicates that it can be null
        public DateTime? ReturnedDate { get; set; }


        //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays?view=netcore-3.1
        public Book(int id, string title, string author,  DateTime publicationDate, DateTime checkedOutDate)
        {
            _id = id;
            _title = title;
            _author = author;
            _publicationDate = publicationDate;

            CheckedOutDate = checkedOutDate;
            DueDate = CheckedOutDate.AddDays(14);
            ReturnedDate = null;

        }
        */

    }
}
