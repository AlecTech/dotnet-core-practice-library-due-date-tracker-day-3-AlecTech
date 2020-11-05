using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPWebMVCBookApp.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(string id, string title, string author, string publicationDate, string checkedOutDate)
        {
            

            if (id != null && title != null && author != null && publicationDate != null && checkedOutDate != null)
            {
                try
                {
                    CreateBook(title, author, publicationDate, checkedOutDate);
                    ViewBag.SuccessfulCreation = true;
                    //You have successfully checked out {title} until {DueDate}."
                    ViewBag.Status = $"Successfully added book ID {id}";
                }
                catch (Exception e)
                {
                    ViewBag.SuccessfulCreation = false;
                    ViewBag.Status = $"An error occured. {e.Message}";
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


        public IActionResult List()
        {

            Debug.WriteLine("ACTION - List Action");
            ViewBag.Books = _context.Books.ToList<Book>();
            ViewBag.Authors = _context.Authors.ToList<Author>();
            return View();
        }
        public IActionResult Details(string id)
        {
            Debug.WriteLine("ACTION - Details Action");
            //ViewBag.Books = Books;
            try
            {
                ViewBag.Book = GetBookByID(id);
                ViewBag.Author = GetBookByID(id);
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
          
            //int parsedID = int.Parse(id);
              //parsedID used to be int.Parse(id)
            // if (!_context.Books.ToList<Book>().Exists(x => x.ID == parsedID))
            // {
                   //Books.Add(new Book(parsedID, title.Trim(), author.Trim(), DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate)));
             //}
           //  else
           //  {
           //      throw new Exception("That Book ID already exists!");
           //  }
         
            Book newBook = new Book {Title = title, PublicationDate = DateTime.Parse(publicationDate)};
            newBook.Author = new AuthorController(_context).GetAuthorByName(author);
            _context.Books.Add(newBook);
            _context.SaveChanges();
            
            //Books.Add(newBook);
          //return newBook;
        }

        public Book GetBookByID(string id)
        {
            Debug.WriteLine($"DATA - GetBookByID({id})");
            return _context.Books.ToList<Book>().Where(x => x.ID == int.Parse(id)).Single();
            //Books.Remove(GetBookByFirstName(id));
        }
        public void ExtendDueDateByID(string id)
        {
            Debug.WriteLine($"DATA - ExtendDueDateByID({id})");
            int BookID = Int32.Parse(id);
            BorrowController.ExtendDueDateForBorrowByID(BookID);
            //GetBookByID(id).DueDate = GetBookByID(id).DueDate.AddDays(14);
            //Books.Remove(GetBookByFirstName(id));
        }


        public void ReturnBookByID(string id)
        {
            Debug.WriteLine($"DATA - ReturnBookByID({id})");
            int BookID = Int32.Parse(id);
            BorrowController.ReturnBorrowByID(BookID);
            //GetBookByID(id).ReturnedDate = DateTime.Today;
            // Books.Remove(GetBookByFirstName(id));
        } 
        public void DeleteBookByID(string id)
        {
            Debug.WriteLine($"DATA - DeleteBookByID({id})");
            _context.Books.ToList<Book>().Remove(GetBookByID(id));
            _context.SaveChanges();
            //Books.Remove(GetBookByFirstName(id));
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
