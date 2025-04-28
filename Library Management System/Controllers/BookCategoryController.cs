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
    public class BookCategoryController : Controller
    {
        // GET: BookCategory
        public ActionResult Index()
        {
            BookCategoryManager obj = new BookCategoryManager();
            List<BookCategory> list = obj.GetBookCategories();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookCategory obj)
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            bookCategoryManager.AddCategory(obj);
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            BookCategory bookCategory= bookCategoryManager.GetBookCategory(id);
            return View(bookCategory);
        }

        [HttpPost]
        public ActionResult Edit(BookCategory obj)
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            bookCategoryManager.UpdateBookCategory(obj);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            BookCategoryManager bookCategoryManager = new BookCategoryManager();
            bookCategoryManager.DeleteBookCategory(id);
            return RedirectToAction("Index");
        }
    }
}