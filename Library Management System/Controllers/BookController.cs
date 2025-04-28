using Library_Management_System.Manager;
using Library_Management_System.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private BookManager bookManager = new BookManager();

        // GET: Books
        public ActionResult Index()
        {
            List<Book> list = bookManager.GetBooks();
            return View(list);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            var book = bookManager.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            BookPublisherManager bookPublisherManager = new BookPublisherManager();

            ViewBag.Categories = bookCategoryManager.GetBookCategories();
            ViewBag.Publishers = bookPublisherManager.GetPublishers();

            return View();
        }


        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            BookPublisherManager bookPublisherManager = new BookPublisherManager();

            if (ModelState.IsValid)
            {
                bookManager.AddBook(book);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = bookCategoryManager.GetBookCategories();
            ViewBag.Publishers = bookPublisherManager.GetPublishers();

            return View();
        }


        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            BookManager bookManager = new BookManager(); // Initialize bookManager here
            Book book = bookManager.GetBook(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            BookPublisherManager bookPublisherManager = new BookPublisherManager();
            ViewBag.Categories = bookCategoryManager.GetBookCategories();
            ViewBag.Publishers = bookPublisherManager.GetPublishers();

            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            BookManager bookManager = new BookManager(); // Initialize bookManager here

            if (ModelState.IsValid)
            {
                bookManager.UpdateBook(book);
                return RedirectToAction("Index");
            }

            // Fetch BookCategory and BookPublisher data
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            BookPublisherManager bookPublisherManager = new BookPublisherManager();
            ViewBag.Categories = bookCategoryManager.GetBookCategories();
            ViewBag.Publishers = bookPublisherManager.GetPublishers();

            return View(book);
        }

        public ActionResult Delete(int id)
        {
            bookManager.DeleteBook(id);
            return RedirectToAction("Index");
        }

    }
}