using Library_Management_System.Manager;
using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BookReturnController : Controller
    {
        // GET: BookReturn
        public ActionResult Index()
        {
            BookReturnManager bookReturnManager = new BookReturnManager();
            List<BookReturn> bookReturns = bookReturnManager.GetBookReturns();
            return View(bookReturns);
        }

        public ActionResult Details(int id)
        {
            BookReturnManager bookReturnManager = new BookReturnManager();
            BookReturn bookReturn = bookReturnManager.GetBookReturn(id);
            if (bookReturn == null)
            {
                return HttpNotFound();
            }
            return View(bookReturn);
        }

        [HttpGet]
    public ActionResult Create()
    {
        MemberManager memberManager = new MemberManager();
        BookIssueManager bookIssueManager = new BookIssueManager();
        ViewBag.BookIssues = new SelectList(bookIssueManager.GetBookIssues().Select(bi => new { Value = bi.IssueID, Text = bi.IssueID.ToString() }), "Value", "Text");
        return View();
    }

        [HttpPost]
        public ActionResult Create(BookReturn bookReturn, string calculateFine)
        {
            BookReturnManager bookReturnManager = new BookReturnManager();
            BookIssueManager bookIssueManager = new BookIssueManager();
            decimal fineAmount = bookReturnManager.CalculateFine(bookReturn.IssueID, bookReturn.ReturnDate);
            bookReturn.FineAmount = fineAmount;

            ViewBag.BookIssues = new SelectList(bookIssueManager.GetBookIssues().Select(bi => new { Value = bi.IssueID, Text = bi.IssueID.ToString() }), "Value", "Text");

            bookReturnManager.AddBookReturn(bookReturn);
            bookReturnManager.UpdateBookIssueReturnDate(bookReturn.IssueID,bookReturn.ReturnDate);
            //bookReturnManager.UpdateBookAvailability(bookReturn.IssueID, true);
            return RedirectToAction("Index");
        }


        // GET: BookReturn/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookReturnManager bookReturnManager = new BookReturnManager();
            BookReturn bookReturn = bookReturnManager.GetBookReturn(id);
            if (bookReturn == null)
            {
                return HttpNotFound();
            }
            BookIssueManager bookIssueManager = new BookIssueManager();
            ViewBag.BookIssues = bookIssueManager.GetBookIssues();
            return View(bookReturn);
        }

        // POST: BookReturn/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookReturn bookReturn)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BookReturnManager bookReturnManager = new BookReturnManager();
                    bookReturnManager.UpdateBookAvailability(bookReturn.IssueID, true);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating book return: " + ex.Message);
                }
            }
            return View(bookReturn);
        }

        // GET: BookReturn/Delete/5
        public ActionResult Delete(int id)
        {
            BookReturnManager bookReturnManager = new BookReturnManager();
            bookReturnManager.DeleteBookReturn(id);
            return View("Index");
        }


    }
}
