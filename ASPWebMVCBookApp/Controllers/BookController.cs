using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ASPWebMVCBookApp.Models;
using ASPWebMVCBookApp.Models.Exceptions;
//using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Schema;
using Microsoft.VisualBasic;
using SQLitePCL;

namespace ASPWebMVCBookApp.Controllers
{
    public class BookController : Controller
    {
    
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        //public string GetDueDate
        //{
        //    get
        //    {
        //        var duedate = "book is not due yet";
        //        if (_context.Borrows.LastOrDefault() != null)
        //        {
        //            duedate = _context.Borrows.LastOrDefault().DueDate.ToLongDateString();
        //        }
        //        return duedate;
        //    }
        //}

        public IActionResult Index()
        {
            //2nd modify return default action to redirect to the “List” action.
            //Debug.WriteLine("ACTION - Index Action");
            return RedirectToAction("List");
        }
  
        /*
        public IActionResult Create()
        {
            //Books.Add(new Book() { ID = "Test1", Title = "Test2" });
            //Books.Add(new Book() { ID = "Oleg", Title = "Eremeev" });
            Debug.WriteLine("ACTION - Create Action");
            ViewBag.Books = Books;
            return View();
        }
        */
        //3rd modify this Create action: 
        public IActionResult Create( string title, string author, string publicationDate, string checkedOutDate)
        {


            //if (title != null && author != null && publicationDate != null && checkedOutDate != null)
            //{
            //    try
            //    {
            //        CreateBook(title, author, publicationDate, checkedOutDate);
            //        ViewBag.SuccessfulCreation = true;
            //        //You have successfully checked out {title} until {DueDate}."
            //        ViewBag.Status = $"Successfully added book {title}";
            //    }
            //    catch (ValidationException e)
            //    {
            //        ViewBag.SuccessfulCreation = false;
            //        ViewBag.Status = $"An error occured. {e.Message}";
            //        ViewBag.Exception = e;
            //    }
            //}
            if (Request.Query.Count > 0)
            {
                try
                {
                    CreateBook(title, author, publicationDate, checkedOutDate);
                    ViewBag.Message = $"Successfully Created Book {title}!";
                }
                // Catch ONLY ValidationException. Any other Exceptions (FormatException, DivideByZeroException, etc) will not get caught, and will break the whole program.
                catch (ValidationException e)
                {
                    ViewBag.Title = title;
                    ViewBag.Name = author;
                    ViewBag.PublicationDate = publicationDate;
                    ViewBag.CheckedOutDate = checkedOutDate;
                    ViewBag.Message = "There exist problem(s) with your submission, see below.";
                    ViewBag.Exception = e;
                    ViewBag.Error = true;
                }
            }

            return View();
            /*
            if (Request.Query.Count > 0)
            {
                Debug.WriteLine("ACTION - Create Action");

                if (id != 0 && author != null && title != null && publicationDate != null && checkedOutDate != null)
                {

                    CreateBook(id, author, title, publicationDate, checkedOutDate);
                    //You have successfully checked out {title} until {DueDate}."
                    Book newBook = CreateBook(id, title, author, publicationDate, checkedOutDate);

                    ViewBag.Good = $"You have successfully checked out {newBook.Title} until {newBook.DueDate}.";

                }
                else
                {
                    throw new Exception("not every field is entered!");
                }
            }
            return View();
            */
        }

        public IActionResult List(string filter)
        {
            //Debug.WriteLine("ACTION - List Action");
            if (filter == "overdue")
            {
                ViewBag.Books = GetOverdueBooks();
            }
            else if (filter == "allbooks")
            {
                ViewBag.Books = GetBooks();
            }
            else
            {
                ViewBag.Books = _context.Books.ToList<Book>();
                ViewBag.Authors = _context.Authors.ToList<Author>();
                ViewBag.Borrows = _context.Borrows.ToList<Borrow>();
                //for dropdown menue
                //ViewBag.Authors = new AuthorController().GetAuthors();
            }


            return View();
        }
        //public IActionResult Find(string id)
        //{
        //    //Debug.WriteLine("ACTION - List Action");
        //    if (filter == "overdue")
        //    {
        //        ViewBag.Books = GetOverdueBooks();
        //    }
        //    else
        //    {
        //        ViewBag.Books = "No books with this ID";
        //    }
        //    return View();
        //}
        public IActionResult Details(string id)
        {
            Debug.WriteLine("ACTION - Details Action");
            //ViewBag.Books = Books;
            try
            {
                ViewBag.Book = GetBookByID(id);
                //ViewBag.Author = GetBookByID(id);
            }
            catch
            {

            }
            return View();
        }
        public IActionResult Extend(string id)
        {
            Debug.WriteLine("ACTION - Extend Action");

            ExtendDueDateByID(id);
            return RedirectToAction("Details", new Dictionary<string, string>() { { "id", id } });
        }
        public IActionResult Return(string id)
        {
            Debug.WriteLine("ACTION - Return Action");

            ReturnBookByID(id);
            return RedirectToAction("Details", new Dictionary<string, string>() { { "id", id } });
        }
        public IActionResult Delete(string id)
        {
            Debug.WriteLine("ACTION - Delete Action");

            DeleteBookByID(id);
            return RedirectToAction("List");
        }
        public IActionResult BorrowBook(string id)
        {
            Debug.WriteLine("ACTION - Delete Action");

            BorrowBookByID(id);
            return RedirectToAction("Details", new Dictionary<string, string>() { { "id", id } });
        }

        //1st modify this empty constructor
        //public static List<Book> Books { get; set; } = new List<Book>()
        ////Dummy data just for test
        //{
        //     //new Book(1, "Test Book", "Test Author", new DateTime(1990, 01, 01), new DateTime(2020, 10, 28)),
        //   // new Book(2, "Another Book", "Test Author", new DateTime(1990, 03, 03), new DateTime(2020, 10, 20))
        //};

        // These methods are for data management. The body of the methods 
        //will be replaced with EF code tomorrow, but for now, we're just using a static list.

        public void CreateBook( string title, string author, string publicationDate, string checkedOutDate)
        {
            //5 parameters comming into this method and passed into List<Book> Books for storage
            //Parsing is done for the API because when it recieves any numeric data it will try to parse it regardless, so we want to 
            //do prasing ourself just to make sure it goes through as expected not as API decodes it on its own 
            Debug.WriteLine($"DATA -  {title}, {author}, {publicationDate}, {checkedOutDate})");
            
            ValidationException exception = new ValidationException();

           
            //parsedID used to be int.Parse(id)
            // if (!_context.Books.ToList<Book>().Exists(x => x.ID == parsedID))
            // {
            //Books.Add(new Book(parsedID, title.Trim(), author.Trim(), DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate)));
            //}
            //  else
            //  {
            //      throw new Exception("That Book ID already exists!");
            //  }

           
            using (var context = new LibraryContext())
            {
                //"Title" Field Validation
                if (string.IsNullOrWhiteSpace(title))
                {
                    exception.ValidationExceptions.Add(new Exception("Title Not Provided"));
                }
                else
                {
                    // Title is a duplicate.
                    // Not a common validation point necessarily, but does perform (2).
                    if (context.Books.Any(x => x.Title.ToUpper().Trim() == title.ToUpper().Trim()))
                    {
                        exception.ValidationExceptions.Add(new Exception("Title Already Exists"));
                    }
                    else
                    {//actual size 100 but for test 30
                        if (title.Length > 30)
                        {
                            // title too long
                            // Common validation point (3).
                            exception.ValidationExceptions.Add(new Exception("The Maximum Length of a Name is 30 Characters"));
                        }
                    }
                }
                //"Author" Field Validation
                if (string.IsNullOrWhiteSpace(author))
                {
                    exception.ValidationExceptions.Add(new Exception("Author name is Not Provided"));
                }
                else
                {
                    // Author does not exist check.
                    // Not a common validation point necessarily, but does perform (2).
                    if (!context.Authors.Any(x => x.Name.ToUpper().Trim().Equals(author.ToUpper().Trim())))
                    {
                        exception.ValidationExceptions.Add(new Exception("This Author doesn't Exists. First You need to add Author(s) in a Different Tab, then a book"));
                    }
                    else
                    {//actual size 60 but for test 15
                        if (author.Length > 15)
                        {
                            // author name too long
                            // Common validation point (3).
                            exception.ValidationExceptions.Add(new Exception("The Maximum Length of Author Name is 15 Characters"));
                        }
                    }
                }
                //"Publication Date" field Validation
                DateTime parsedDate1;

                //bool parsedDate1 = DateTime.TryParse(publicationDate, out DateTime parsedDate1);

                if (string.IsNullOrWhiteSpace(publicationDate))
                {
                    exception.ValidationExceptions.Add(new Exception("PublicationDate Not Provided"));
                }
                else
                {
                    // Category ID fails parse.
                  
                    if (!DateTime.TryParse(publicationDate, out parsedDate1))
                    {
                        exception.ValidationExceptions.Add(new Exception("PublicationDate Not Valid"));
                    }
                    else
                    {
                        if (DateTime.Parse(checkedOutDate) < DateTime.Parse(publicationDate))
                        {
                            exception.ValidationExceptions.Add(new Exception("CheckedOutDate cannot be prior to PublicationDate"));
                        }
                        else
                        {
                            if (DateTime.Parse(publicationDate) > DateTime.Today)
                            {
                                exception.ValidationExceptions.Add(new Exception("PublishedDate cannot be in the future"));
                            }
                        }
                    }
                }
                //"CheckedOutDate" field Validation
                DateTime parsedDate2;

                if (string.IsNullOrWhiteSpace(checkedOutDate))
                {
                    exception.ValidationExceptions.Add(new Exception("CheckedOutDate Not Provided"));
                }
                else
                {
                    if (!DateTime.TryParse(checkedOutDate, out parsedDate2))
                    {
                        exception.ValidationExceptions.Add(new Exception("CheckedOutDate Not Valid"));
                    }
                    else
                    {
                        if (DateTime.Parse(checkedOutDate) < DateTime.Parse(publicationDate))
                        {
                            exception.ValidationExceptions.Add(new Exception("CheckedOutDate cannot be prior to PublicationDate"));
                        }
                        else
                        {
                            if (DateTime.Parse(publicationDate) > DateTime.Today)
                            {
                                exception.ValidationExceptions.Add(new Exception("PublishedDate cannot be in the future"));
                            }
                        }
                    }
                }

                //Checkout date < than Publication Date test and PublicationDate into the future test
                //if (DateTime.Parse(checkedOutDate) < DateTime.Parse(publicationDate))
                //{
                //    exception.ValidationExceptions.Add(new Exception("CheckedOutDate cannot be prior to PublicationDate"));
                //}
                //else
                //{
                //    if (DateTime.Parse(publicationDate) > DateTime.Today)
                //    {
                //        exception.ValidationExceptions.Add(new Exception("PublishedDate cannot be in the future"));
                //    }
                //}

            }


            if (exception.ValidationExceptions.Count >0)
            {
                throw exception;
            }

            Book newBook = new Book {Title = title, PublicationDate = DateTime.Parse(publicationDate)};
            //Borrow newBorrow = new Borrow { CheckedOutDate = DateTime.Parse(checkedOutDate), DueDate = DateTime.Parse(checkedOutDate).AddDays(7) };
            //Join 2 tables data
            //newBook.Borrows.Add(newBorrow);
            newBook.Author = new AuthorController(_context).GetAuthorByName(author);
            _context.Books.Add(newBook);
            _context.SaveChanges();
            
            //Books.Add(newBook);
          //return newBook;
        }
        public Book GetBookByID(string id)
        {
            Debug.WriteLine($"DATA - GetBookByID({id})");

            ValidationException exception = new ValidationException();
            using (var context = new LibraryContext())
            {

                //
                int parsedID = 0;
                if (string.IsNullOrWhiteSpace(id))
                {
                    exception.ValidationExceptions.Add(new Exception("ID Not Provided"));
                }
                else
                {
                    // Category ID fails parse.
                    // Common validation points (5) and (5a).
                    if (!int.TryParse(id, out parsedID))
                    {
                        exception.ValidationExceptions.Add(new Exception("ID Not Valid, please enter intiger"));
                    }
                    else
                    {
                        // Category ID exists.
                        // Common validation point (7).
                        if (!context.Books.Any(x => x.ID == parsedID))
                        {
                            exception.ValidationExceptions.Add(new Exception("ID Does Not Exist"));
                        }
                    }

                }
            }

              if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }
            return _context.Books.Include(x => x.Author).Include(x => x.Borrows).Where(x => x.ID == int.Parse(id)).Single();
        }
        public void ExtendDueDateByID(string id)
        {
            Debug.WriteLine($"DATA - ExtendDueDateByID({id})");
            int BookID = Int32.Parse(id);
            new BorrowController(_context).ExtendDueDateForBorrowByID(BookID);
            //GetBookByID(id).DueDate = GetBookByID(id).DueDate.AddDays(14);
            //Books.Remove(GetBookByFirstName(id));
        }
        public void ReturnBookByID(string id)
        {
            Debug.WriteLine($"DATA - ReturnBookByID({id})");
            int BookID = Int32.Parse(id);
            new BorrowController(_context).ReturnBorrowByID(BookID);
            //GetBookByID(id).ReturnedDate = DateTime.Today;
            // Books.Remove(GetBookByFirstName(id));
        } 
        public void DeleteBookByID(string id)
        {
            Debug.WriteLine($"DATA - DeleteBookByID({id})");
            _context.Books.Remove(GetBookByID(id));
            _context.SaveChanges();
            //Books.Remove(GetBookByFirstName(id));
        }
        public void BorrowBookByID(string id)
        {
            new BorrowController(_context).CreateBorrow(Int32.Parse(id));
        }
        public List<Book> GetBooks()
        {
            List<Book> results;
            using (LibraryContext context = new LibraryContext())
            {
                //results = _context.Books.Include(x => x.Author).Include(x => x.Borrows).Where(x => x.Borrows.Any(y => y.Book.Title != null)).ToList();
                results = _context.Books.Include(x => x.Author).Include(x => x.Borrows).ToList();
                return results;
            }
        }
        public List<Book> GetOverdueBooks()
        {          
            List<Book> results;         
            DateTime date1 = new DateTime();
            DateTime date2 = date1.Date;
            string date_str = date2.ToString();
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            var parsedDate = DateTime.Parse(date);
            //❏	mvc_library DB find all past due dates for books	
            //❏	SELECT *
            //❏	FROM author
            //❏	INNER JOIN book ON author.ID = book.AuthorID
            //❏	INNER JOIN borrow ON book.ID = borrow.BookID
            //❏	WHERE borrow.DueDate < NOW() AND borrow.ReturnedDate IS NULL;
            using (LibraryContext context = new LibraryContext())
            {
                //results = _context.Books.Include(x => x.Borrows).Where(x => DateTime.Parse(x.GetDueDate) < parsedDate).ToList(); //DateTime.Parse(x.GetDueDate)
                //results = _context.Books.Include(x => x.GetDueDate).Where(x => DateTime.Parse(x.GetDueDate) < ).ToList(); //
                //I got a help with This Subquery Logic from Aaron Barthel
                 results = _context.Books.Include(x => x.Author).Include(x => x.Borrows).Where(x => x.Borrows.Any(y => y.DueDate < parsedDate && y.ReturnedDate == null)).ToList();
                return results;
            }
        }
        /*
        public Book GetBookByFirstName(int id)
        {
            Debug.WriteLine($"DATA - GetBookByFirstName({id})");
            // This assumes nobody's name is duplicated. If it is, it will return null.
            return Books.Where(x => x.ID.Trim().ToUpper() == id.Trim().ToUpper()).SingleOrDefault();
        }
        */
    }
}
