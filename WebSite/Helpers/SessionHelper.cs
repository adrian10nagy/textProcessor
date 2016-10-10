using BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    public class SessionHelper : Controller
    {
        #region GetUserFields

        public string Id()
        {
            return Session.SessionID;
        }

        public int UserId()
        {
            if (System.Web.HttpContext.Current.Session["userId"] == null || int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()) == 0)
            {
                return 0;
            }

            return int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());
        }

        public UserType UserType()
        {
            if (System.Web.HttpContext.Current.Session["userType"] == null || int.Parse(System.Web.HttpContext.Current.Session["userType"].ToString()) == 0)
            {
                return BL.Entities.UserType.Prospect;
            }

            return (UserType)int.Parse(System.Web.HttpContext.Current.Session["userType"].ToString());
        }

        public string UserFirstName()
        {
            if (System.Web.HttpContext.Current.Session["userFirstName"] == null || System.Web.HttpContext.Current.Session["userFirstName"].ToString() == string.Empty)
            {
                return string.Empty;
            }

            return System.Web.HttpContext.Current.Session["userFirstName"].ToString();
        }

        public string UserLastName()
        {
            if (System.Web.HttpContext.Current.Session["userLastName"] == null || System.Web.HttpContext.Current.Session["userLastName"].ToString() == string.Empty)
            {
                return string.Empty;
            }

            return System.Web.HttpContext.Current.Session["userLastName"].ToString();
        }

        #endregion

        public UserViewModel User()
        {
            UserViewModel user = new UserViewModel();
            user.Id = UserId();
            user.FirstName = UserFirstName();
            user.LastName = UserLastName();
            user.UserType = UserType();

            if (user.Id == 0 || user.FirstName == string.Empty || user.LastName == string.Empty)
            {
                return null;
            }

            return user;
        }

        public void setUser(UserViewModel user)
        {
            System.Web.HttpContext.Current.Session["userId"] = user.Id;
            System.Web.HttpContext.Current.Session["userType"] = (int)user.UserType;
            System.Web.HttpContext.Current.Session["userFirstName"] = user.FirstName;
            System.Web.HttpContext.Current.Session["userLastName"] = user.LastName;
        }

        #region unserUserFields

        public void unsetUserType()
        {
            System.Web.HttpContext.Current.Session.Remove("userType");
        }

        public void unsetUserId()
        {
            System.Web.HttpContext.Current.Session.Remove("userId");
        }

        public void unsetUserFirstName()
        {
            System.Web.HttpContext.Current.Session.Remove("userFirstName");
        }

        public void unsetUserLastName()
        {
            System.Web.HttpContext.Current.Session.Remove("userLastName");
        }

        #endregion

        public void unsetUser()
        {
            unsetUserId();
            unsetUserType();
            unsetUserFirstName();
            unsetUserLastName();
        }
    }
}