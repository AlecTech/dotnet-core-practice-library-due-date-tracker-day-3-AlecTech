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
            Debug.WriteLine("ACTION - Index Action");
            return View();
        }
        /*
         *  ❏	“BookController”(like our PersonController) Class which inherits from “Controller”:
            ❏	A public static “Books” property which is a list of “Book”(like List<People>) objects. (will be created in Models like person class)
            ❏	Action/View “Index”. (redirects to ListAction)(Like index to Management)
                ●	Modify this default action to redirect to the “List” action.
            ❏	Action/View “Create” (This will have a FORM, it will call Book method through the action then submitted)
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

        public IActionResult Management()
        {
            //People.Add(new Person() { FirstName = "Test1", LastName = "Test2" });
            //People.Add(new Person() { FirstName = "Oleg", LastName = "Eremeev" });
            Debug.WriteLine("ACTION - Management Action");
            ViewBag.Books = Books;
            return View();
        }
        public IActionResult Create(string firstName, string lastName)
        {
            Debug.WriteLine("ACTION - Create Action");

            CreateBook(firstName, lastName);
            return RedirectToAction("Management");
        }

        public IActionResult Delete(string firstName)
        {
            Debug.WriteLine("ACTION - Delete Action");

            DeleteBookByFirstName(firstName);
            return RedirectToAction("Management");
        }

        //1st modify this line
        public static List<Book> Books = new List<Book>();
        // These methods are for data management. The body of the methods 
        //will be replaced with EF code tomorrow, but for now, we're just using a static list.
        public void CreateBook(string firstName, string lastName)
        {
            Debug.WriteLine($"DATA - CreateBook({firstName}, {lastName})");
            Books.Add(new Book()
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim()
            });
        }

        public void DeleteBookByFirstName(string firstName)
        {
            Debug.WriteLine($"DATA - DeleteBookByFirstName({firstName})");
            Books.Remove(GetBookByFirstName(firstName));
        }

        public Book GetBookByFirstName(string firstName)
        {
            Debug.WriteLine($"DATA - GetBookByFirstName({firstName})");
            // This assumes nobody's name is duplicated. If it is, it will return null.
            return Books.Where(x => x.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper()).SingleOrDefault();
        }
    }
}
