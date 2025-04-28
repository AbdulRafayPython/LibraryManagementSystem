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
    public class FinePolicyController : Controller
    {

        private FinePolicyManager finePolicyManager = new FinePolicyManager();

        // GET: FinePolicy
        public ActionResult Index()
        {
            List<FinePolicy> finePolicies = finePolicyManager.GetFinePolicies();
            return View(finePolicies);
        }

        // GET: FinePolicy/Details/5
        public ActionResult Details(int id)
        {
            FinePolicy finePolicy = finePolicyManager.GetFinePolicy(id);
            if (finePolicy == null)
            {
                return HttpNotFound();
            }
            return View(finePolicy);
        }

        // GET: FinePolicy/Create
        public ActionResult Create()
        {
            UserTypeManager userTypeManager= new UserTypeManager();
            ViewBag.MemberTypes = userTypeManager.GetUserTypes();
            return View();
        }

        // POST: FinePolicy/Create
        [HttpPost]
        public ActionResult Create(FinePolicy finePolicy)
        {
            if(ModelState.IsValid)
            { 
                finePolicyManager.AddFinePolicy(finePolicy);
                return RedirectToAction("Index");
            }
            UserTypeManager userTypeManager = new UserTypeManager();
            ViewBag.MemberTypes = userTypeManager.GetUserTypes();
            return View(finePolicy);
        }

        // GET: FinePolicy/Edit/5
        public ActionResult Edit(int id)
        { 
            FinePolicy finePolicy= finePolicyManager.GetFinePolicy(id);
            UserTypeManager userTypeManager = new UserTypeManager();
            ViewBag.MemberTypes = userTypeManager.GetUserTypes();
            return View(finePolicy);
        }

        // POST: FinePolicy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FinePolicy finePolicy)
        {
            finePolicyManager.UpdateFinePolicy(finePolicy);
            return RedirectToAction("Index");
        }

        // GET: FinePolicy/Delete/5
        public ActionResult Delete(int id)
        {
            FinePolicy finePolicy = finePolicyManager.GetFinePolicy(id);
            if (finePolicy == null)
            {
                return HttpNotFound();
            }
            return View(finePolicy);
        }

        // POST: FinePolicy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                finePolicyManager.DeleteFinePolicy(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Delete", finePolicyManager.GetFinePolicy(id));
            }
        }
    }
}
