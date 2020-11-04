using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASPWebMVCBookApp.Models
{
    public class Book
    {
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
    }
}
