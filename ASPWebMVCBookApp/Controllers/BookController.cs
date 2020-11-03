using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPWebMVCBookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ASPWebMVCBookApp.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            //2nd modify return default action to redirect to the “List” action.
            //Debug.WriteLine("ACTION - Index Action");
            return RedirectToAction("List");
        }
        /*
         *  ❏	“BookController”(like our PersonController) Class which inherits from “Controller”:
            ❏	A public static “Books” property which is a list of “Book”(like List<People>) objects. (will be created in Models like person class)
            ❏	Action/View “Index”. (redirects to ListAction)(Like index to Management)
                ●	Modify this default action to redirect to the “List” action.
            ❏	Action/View “Create” (This will have a FORM, it will call Book method through the action when submitted)
                ●	Will display the form to create an object. 
                ●	Call the “CreateBook()” method with the supplied query string parameters.
                ●	Handle any exceptions thrown by “CreateBook()”;
                ●	Success Message: "You have successfully checked out {title} until {DueDate}."(conditional @if)
                ●	Error Message: “Unable to check out book: {Exception.Message}.”
            ❏	Action/View “List”
                ●	Render a list of all books as links in the format “{Title} by {Author}” that will load the “Details” Action/View for that book when clicked. (List of Books as LINQ) Ancer Tag to Details view with get parameter in the URL. (ex: Linking bookController details. Link to Details Page add a question mark and specify ID is = to whatever number book was clicked. (Getter property)
            ❏	Action/View “Details” (if no ID => say “no books selected”) otherwise display book that we selected. 
                ●	If no query string parameter “id” was supplied, render “No book selected.”
                ●	If an “id” query string parameter was supplied, use “GetBookByID()” and render:
                ●	"You checked out {Title} on {CheckedOutDate}, and the due date is {DueDate}."
                ●	A button that will call “ExtendDueDateForBookByID()”.
                ●	A button that will call “ReturnBookByID()”.
                ●	A button that will call “DeleteBookByID()”.

         */
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
                    CreateBook(id, title, author, publicationDate, checkedOutDate);
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
            ViewBag.Books = Books;

            return View();
        }
        public IActionResult Details(string id)
        {
            Debug.WriteLine("ACTION - Details Action");
            //ViewBag.Books = Books;
            try
            {
                ViewBag.Book = GetBookByID(id);
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
        public static List<Book> Books { get; set; } = new List<Book>()
        //Dummy data just for test
        {
             new Book(1, "Test Book", "Test Author", new DateTime(1990, 01, 01), new DateTime(2020, 10, 28)),
            new Book(2, "Another Book", "Test Author", new DateTime(1990, 03, 03), new DateTime(2020, 10, 20))
        };

        // These methods are for data management. The body of the methods 
        //will be replaced with EF code tomorrow, but for now, we're just using a static list.

        public void CreateBook(string id, string title, string author, string publicationDate, string checkedOutDate)
        {
            //5 parameters comming into this method and passed into List<Book> Books for storage
            //Parsing is done for the API because when it recieves any numeric data it will try to parse it regardless, so we want to 
            //do prasing ourself just to make sure it goes through as expected not as API decodes it on its own 
            Debug.WriteLine($"DATA - CreateBook({id}, {title}, {author}, {publicationDate}, {checkedOutDate})");
            int parsedID = int.Parse(id);
             //parsedID used to be int.Parse(id)
            if (!Books.Exists(x => x.ID == parsedID))
            {
                Books.Add(new Book(parsedID, title.Trim(), author.Trim(), DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate)));
            }
            else
            {
                throw new Exception("That Book ID already exists!");
            }
            //Book aBook = new Book(id, title, author,  publicationDate, checkedOutDate);
            // Books.Add(aBook);
            //return aBook;
        }


        
        public Book GetBookByID(string id)
        {
            Debug.WriteLine($"DATA - GetBookByID({id})");
            return Books.Where(x => x.ID == int.Parse(id)).Single();
            //Books.Remove(GetBookByFirstName(id));
        }
        public void ExtendDueDateByID(string id)
        {
            Debug.WriteLine($"DATA - ExtendDueDateByID({id})");
            GetBookByID(id).DueDate = GetBookByID(id).DueDate.AddDays(14);
            //Books.Remove(GetBookByFirstName(id));
        }
        public void ReturnBookByID(string id)
        {
            Debug.WriteLine($"DATA - ReturnBookByID({id})");
            GetBookByID(id).ReturnedDate = DateTime.Today;
            // Books.Remove(GetBookByFirstName(id));
        } 
        public void DeleteBookByID(string id)
        {
            Debug.WriteLine($"DATA - DeleteBookByID({id})");
            Books.Remove(GetBookByID(id));
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
