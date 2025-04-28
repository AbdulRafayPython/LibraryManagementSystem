using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Management_System.Models;
using Library_Management_System.Manager;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class UserTypeController : Controller
    {
        // GET: UserType
        public ActionResult Index(UserType userType)
        {
            UserTypeManager obj=new UserTypeManager();
            List<UserType> list = obj.GetUserTypes();
            return View(list);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserType userType)
        {
            UserTypeManager obj = new UserTypeManager();
            obj.AddUserType(userType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserTypeManager userTypeManager = new UserTypeManager();
            UserType obj = userTypeManager.GetUserType(id);

            if (obj == null)
            {
                return HttpNotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(UserType userType)
        {
            UserTypeManager userTypeManager = new UserTypeManager();
            userTypeManager.UpdateUsertype(userType);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            UserTypeManager userTypeManager = new UserTypeManager();
            userTypeManager.DeleteUserType(id);
            return RedirectToAction("Index");
        }



    }
}