using Library_Management_System.Manager;
using Library_Management_System.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BookIssueController : Controller
    {
        // GET: BookIssue
        public ActionResult Index()
        {
            BookIssueManager obj = new BookIssueManager();
            List<BookIssue> list = obj.GetBookIssues();
            return View(list);

        }

        [HttpGet]
        public ActionResult Create()
        {
            MemberManager memberManager = new MemberManager();
            BookManager bookManager=new BookManager();
            LoginManager loginManager =new LoginManager();
            ViewBag.Members = new SelectList(memberManager.GetMembers().Select(m => new { Value = m.MemID, Text = m.FName + " " + m.LName }), "Value", "Text");
            ViewBag.Books = bookManager.GetBooks();
            ViewBag.IssuedByID = loginManager.GetUsers();

            return View();
        }

        [HttpPost]
        public ActionResult Create(BookIssue bookIssue)
        {
            if (ModelState.IsValid)
            {
                BookIssueManager bookIssueManager=new BookIssueManager();
                bookIssueManager.CreateBookIssue(bookIssue);
                return RedirectToAction("Index");
            }

            MemberManager memberManager = new MemberManager();
            BookManager bookManager = new BookManager();
            LoginManager loginManager = new LoginManager();
            ViewBag.Members = memberManager.GetMembers();
            ViewBag.Books = bookManager.GetBooks();
            ViewBag.IssuedByID = loginManager.GetUsers(); 
            return View(bookIssue);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookIssueManager bookIssueManager= new BookIssueManager();
            BookIssue bookIssue= bookIssueManager.GetBookIssue(id);
            MemberManager memberManager= new MemberManager();
            BookManager bookManager = new BookManager();
            LoginManager loginManager = new LoginManager();

            ViewBag.Members = new SelectList(memberManager.GetMembers().Select(m => new { Value = m.MemID, Text = m.FName + " " + m.LName }), "Value", "Text");
            ViewBag.Books = bookManager.GetBooks();
            ViewBag.IssuedByID = loginManager.GetUsers();
            return View(bookIssue);

        }

        [HttpPost]
        public ActionResult Edit(BookIssue bookIssue)
        {
            BookIssueManager bookIssueManager = new BookIssueManager();
            bookIssueManager.UpdateBookIssue(bookIssue);
            return View(bookIssue);
        }

        public ActionResult Delete(int id)
        {
            BookIssueManager bookIssueManager=new BookIssueManager();
            bookIssueManager.DeleteBookIssue(id);
            return RedirectToAction("Index");
        }
    }
}