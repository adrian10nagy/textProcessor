using BL.Business;
using BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Inregistrare()
        {
            return View();
        }

        public ActionResult RecupereazaParola()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecupereazaParola(FormCollection collection)
        {
            if (collection["recoverUserEmail"] == null)
            {
                return RedirectToAction("LostPassword", "User");
            }

            var userEmail = collection["recoverUserEmail"].ToString();
            var user = KitBL.Instance.UserBL.GetByEmail(userEmail);

            if (user != null && user.Email != "" && user.Email != null)
            {
                var newPassword = user.PasswordHashed + "NewPassword";
                //SMTP.sendEmail("info@knowledgeshare.ro", user.Email, "Password change request!", "Please change your password, visit www.Knowledgeshare.ro/User/ChangePassword . Your temporary password is:" + newPassword);
            }
            return RedirectToAction("Index", "Home");
        }
        

        [HttpPost]
        public ActionResult Inregistrare(FormCollection collection)
        {
            if(collection["registerUserEmail"] == string.Empty ||
                collection["registerUserPassword"] == string.Empty ||
                collection["registerUserName"] == string.Empty)
            {
                return RedirectToAction("Index", "Home");
            }

            UserViewModel user = new UserViewModel
            {
                Email = collection["registerUserEmail"],
                PasswordHashed = collection["registerUserPassword"],
                FirstName = collection["registerUserName"],
                LastName = "Guest",
                Flags = UserFlagsBL.Default,
                JoinDate = DateTime.Now,
                UserType = UserType.Member,
            };

            KitBL.Instance.UserBL.AddNewUser(user);

             var success = AsyncUserLogin(user.Email, user.PasswordHashed);
            // var emailTemplate = KitBL.Instance.EmailTemplate.GetByName("GeneralRegister");
            // SMTP.sendEmail("info@knowledgeshare.ro", user.Email, emailTemplate.Subject, emailTemplate.Structure);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user = KitBL.Instance.UserBL.ValidateLogin(collection["loginUserEmail"], collection["loginUserPassword"]);

            if (user != null && user.Id > 0)
            {
                BaseMVC.setUser(user);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("userId");
            Session.Remove("userFirstName");
            Session.Remove("userLastName");
            Session.Remove("userType");

            BaseMVC.unsetUser();
            WebSite.Helpers.Web.Instance.Session.unsetUser();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AsyncUserLogin(string userEmail, string userPass)
        {
            var user = KitBL.Instance.UserBL.ValidateLogin(userEmail, userPass);

            if (user != null && user.Id > 0)
            {
                Session["userId"] = user.Id;
                Session["userType"] = (int)user.UserType;
                Session["userFirstName"] = user.FirstName;
                Session["userLastName"] = user.LastName;

                //Set login credentials
                BaseMVC.setUser(user);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}