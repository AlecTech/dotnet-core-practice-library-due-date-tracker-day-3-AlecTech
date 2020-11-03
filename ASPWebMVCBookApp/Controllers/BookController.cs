using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPWebMVCBookApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebMVCBookApp.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            //2nd modify return default action to redirect to the “List” action.
            Debug.WriteLine("ACTION - Index Action");
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
        public IActionResult Create(int id, string author, string title, DateTime publicationDate, DateTime checkedOutDate)
        {
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
        }

        public Book CreateBook(int id, string author, string title, DateTime publicationDate, DateTime checkedOutDate)
        {
            Debug.WriteLine($"DATA - CreateBook({id}, {author}, {title}, {publicationDate}, {checkedOutDate})");

            Book aBook = new Book(id, author, title, publicationDate, checkedOutDate);
            Books.Add(aBook);
            return aBook;
        }
        /*
        public IActionResult Delete(int id)
        {
            Debug.WriteLine("ACTION - Delete Action");

            DeleteBookByFirstName(id);
            return RedirectToAction("");
        }
        */
        //1st modify this line
        public static List<Book> Books { get; set; } = new List<Book>();
        // These methods are for data management. The body of the methods 
        //will be replaced with EF code tomorrow, but for now, we're just using a static list.
    
        /*
        public void DeleteBookByFirstName(int id)
        {
            Debug.WriteLine($"DATA - DeleteBookByFirstName({id})");
            Books.Remove(GetBookByFirstName(id));
        }
        */
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
