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
    public class LibrarianController : Controller
    {
        // GET: Librariam
        public ActionResult List()
        {
            LibrarianManager obj = new LibrarianManager();
            List<Librarian> list = obj.GetLibrarians();
            return View(list);
        }
    }
}