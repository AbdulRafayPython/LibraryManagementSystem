using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Management_System.Manager;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BookPublisherController : Controller
    {
        // GET: BookPublisher
        public ActionResult Index()
        {
            BookPublisherManager manager = new BookPublisherManager();
            List<BookPublisher> publishers = manager.GetPublishers();
            return View(publishers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookPublisher publisher)
        {
            if (ModelState.IsValid)
            {
                BookPublisherManager manager = new BookPublisherManager();
                manager.AddPublisher(publisher);
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookPublisherManager manager = new BookPublisherManager();
            BookPublisher publisher = manager.GetPublisher(id);
            return View(publisher);
        }

        [HttpPost]
        public ActionResult Edit(BookPublisher publisher)
        {
            if (ModelState.IsValid)
            {
                BookPublisherManager manager = new BookPublisherManager();
                manager.UpdatePublisher(publisher);
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        public ActionResult Delete(int id)
        {
            BookPublisherManager manager = new BookPublisherManager();
            manager.DeletePublisher(id);
            return RedirectToAction("Index");
        }
    }
}