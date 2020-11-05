using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPWebMVCBookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPWebMVCBookApp.Controllers
{
    public class BorrowController : Controller
    {

        private static LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
      
        public static void ExtendDueDateForBorrowByID(int BookID)
        {
            //_context.Borrows.FirstOrDefault<Borrow>(e => e.BookID == BookID).DueDate.AddDays(7);

            using (var context = new LibraryContext())
            {
                Borrow borrow = context.Borrows.Where(x => x.ID == BookID).Single();
                borrow.DueDate = borrow.DueDate.AddDays(7);
                context.SaveChanges();
            }
            //private static Borrow GetById(LibraryContext context, int id)
            //{
                
            //}

            //using (var _context = new LibraryContext())
            //{
            //    return _context.Books
            //                    .Include(x => x.Author)
            //                    .Include(x => x.Borrows)
            //                    .Where(x => x.Borrows.Any(y => y.DueDate < DateTime.Today))
            //                    .ToList();
            //}
        }
        public static void ReturnBorrowByID(int BookID)
        {
            //_context.Borrows.FirstOrDefault<Borrow>(e => e.BookID == BookID).ReturnedDate = DateTime.Today;
            using (var context = new LibraryContext())
            {
                Borrow borrow = context.Borrows.Where(x => x.ID == BookID).Single();
                borrow.ReturnedDate = (DateTime)(borrow.ReturnedDate = DateTime.Today);
                context.SaveChanges();
            }
        }

        public void CreateBorrow(int BookID)
        {
            _context.Borrows.Add(new Borrow { BookID = BookID, CheckedOutDate = DateTime.Today, DueDate = DateTime.Today.AddDays(14) });
            _context.SaveChanges();
        }

    }
}
