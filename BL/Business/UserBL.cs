using BL.Entities;
using DAL.Entities;
using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business
{
    public class UserBL
    {
        public UserViewModel ValidateLogin(string email, string password)
        {
            var user = Kit.Instance.Users.GetUserByEmailPass(email, password);

            UserViewModel userView = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email,
                UserType = (BL.Entities.UserType)user.UserType
            };

            return userView;
        }

        public void AddNewUser(UserViewModel user)
        {
            User u = new User()
            {
                Email = user.Email,
                FirstName = user.Email,
                Flags = (UserFlags)(int)user.Flags,
                JoinDate = user.JoinDate,
                LastName = user.LastName,
                PasswordHashed = user.PasswordHashed,
                UserType = (DAL.Entities.UserType)(int)user.UserType,
                EmailPreference = EmailPreferenceBL.All
            };
            Kit.Instance.Users.Insert(u);
        }

        public User GetByEmail(string email)
        {
            User user = DAL.SDK.Kit.Instance.Users.GetUserByEmail(email);

            return user;
        }
    }
}
