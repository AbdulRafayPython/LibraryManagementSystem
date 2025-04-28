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
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            AuthorManager obj = new AuthorManager();
            List<Author> list = obj.GetAuthors();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author obj)
        {
            if (ModelState.IsValid)
            {
                AuthorManager authorManager = new AuthorManager();
                authorManager.AddAuthor(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AuthorManager authorManager = new AuthorManager();
            Author author = authorManager.GetAuthor(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                AuthorManager authorManager = new AuthorManager();
                authorManager.UpdateAuthor(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Delete(int id)
        {
            AuthorManager authorManager = new AuthorManager();
            authorManager.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}