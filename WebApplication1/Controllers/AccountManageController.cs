using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountManageController : Controller
    {
        // GET: AccountManage
        MobileShopEntities db = new MobileShopEntities();
        public ActionResult Log_In_Screen()
        {
            return View();
        }

        public ActionResult Sign_Up_Screen()
        {
            return View();
        }
        public ActionResult Log_In(ModelAccount model)
        {
            Account acc = db.Accounts.Where(x => x.username == model.username && x.password == model.password).FirstOrDefault();

            if (acc == null)
            {
                return RedirectToAction("Log_In_Screen", "AccountManage");
            }
            else
            {
                //Login Success
                Session["account"] = acc;
                if (acc.role == "admin")
                {
                    return RedirectToAction("Index", "ProductManage");
                }
                else if (acc.role == "user")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Log_In_Screen", "AccountManage");
                }
            }
        }

        public ActionResult CheckPassword (ModelAccount model)
        {
            if (model.password != model.repassword)
            {
                return RedirectToAction("Sign_Up_Screen", "AccountManage");
            }
            else
            {
                return RedirectToAction("Create_NewAccount", "AccountManage");
            }
        }

        public ActionResult Create_NewAccount(ModelAccount model)
        {
            model.role = "user";
            Account new_acc = new Account()
                {
                    username = model.username,
                    password = model.password,
                    role = model.role,
                };
                db.Accounts.Add(new_acc);
                db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}