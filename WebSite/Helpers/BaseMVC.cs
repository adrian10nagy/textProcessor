using BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Helpers
{
    public class BaseMVC
    {
        private static UserViewModel _instance = new UserViewModel();
        public static UserViewModel User;

        static UserViewModel getInstance()
        {
            return WebSite.Helpers.Web.Instance.Session.User();
        }

        public static bool IsLoggedIn()
        {
            User = getInstance();
            if (User == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsAdmin()
        {
            if (!IsLoggedIn())
                return false;

            if (User.UserType == UserType.Admin)
            {
                return true;
            }
            return false;
        }

        public static UserViewModel getUser()
        {
            if (!IsLoggedIn())
            {
                return null;
            }

            return User;
        }

        public static int getUserId()
        {
            if (!IsLoggedIn())
            {
                return 0;
            }

            return User.Id;
        }

        public static void setUser(UserViewModel user)
        {
            WebSite.Helpers.Web.Instance.Session.setUser(user);
        }

        public static void unsetUser()
        {
            WebSite.Helpers.Web.Instance.Session.unsetUser();
        }
    }
}