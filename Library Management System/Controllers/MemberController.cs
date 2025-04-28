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
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            MemberManager obj = new MemberManager();
            List<Member> list = obj.GetMembers();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MemberManager memberManager= new MemberManager();
            ViewBag.MemberTypes = memberManager.GetMemberTypes();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                MemberManager memberManager = new MemberManager();
                memberManager.AddMember(member);
                return RedirectToAction("Index");
            }
            UserTypeManager userTypeManager = new UserTypeManager();
            ViewBag.MemberTypes = userTypeManager.GetUserTypes();
            return View(member);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            MemberManager memberManager = new MemberManager();
            Member member = memberManager.GetMember(id);
            UserTypeManager userTypeManager = new UserTypeManager();
            ViewBag.MemberTypes = userTypeManager.GetUserTypes();
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            MemberManager memberManager= new MemberManager();
            memberManager.UpdateMember(member);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MemberManager memberManager = new MemberManager();
            memberManager.DeleteMember(id);
            return RedirectToAction("Index");
        }
    }
}