using Library_Management_System.Manager;
using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BookAuthorController : Controller
    {
        private BookAuthorManager bookAuthorManager = new BookAuthorManager();
        private BookManager bookManager = new BookManager();
        private AuthorManager authorManager = new AuthorManager();

        // GET: BookAuthors
        public ActionResult Index()
        {
            List<BookAuthor> list = bookAuthorManager.GetBookAuthors();
            return View(list);
        }

        // GET: BookAuthors/Create
        public ActionResult Create()
        {
            ViewBag.Books = bookManager.GetBooks();
            ViewBag.Authors = authorManager.GetAuthors();
            return View();
        }

        // POST: BookAuthors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                bookAuthorManager.AddBookAuthor(bookAuthor);
                return RedirectToAction("Index");
            }

            ViewBag.Books = bookManager.GetBooks();
            ViewBag.Authors = authorManager.GetAuthors();
            return View();
        }

        // GET: BookAuthors/Edit/5
        public ActionResult Edit(int bookId, int authorId)
        {
            var bookAuthor = bookAuthorManager.GetBookAuthor(bookId, authorId);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }

            ViewBag.Books = new SelectList(bookManager.GetBooks(), "BookID", "BookTitle", bookAuthor.BookID);
            ViewBag.Authors = new SelectList(authorManager.GetAuthors(), "AuthorID", "AuthorName", bookAuthor.AuthorID);
            return View(bookAuthor);
        }

        // POST: BookAuthors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                bookAuthorManager.UpdateBookAuthor(bookAuthor);
                return RedirectToAction("Index");
            }

            ViewBag.Books = new SelectList(bookManager.GetBooks(), "BookID", "BookTitle", bookAuthor.BookID);
            ViewBag.Authors = new SelectList(authorManager.GetAuthors(), "AuthorID", "AuthorName", bookAuthor.AuthorID);
            return View(bookAuthor);
        }

        // GET: BookAuthors/Delete/5
        public ActionResult Delete(int bookId, int authorId)
        {
            var bookAuthor = bookAuthorManager.GetBookAuthor(bookId, authorId);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }

            bookAuthorManager.DeleteBookAuthor(bookId, authorId);
            return RedirectToAction("Index");
        }

    }
}