using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Entities
{
    public class UserViewModel
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string Email;
        public string PasswordHashed;
        public UserFlagsBL Flags;
        public DateTime JoinDate;

        public UserType UserType;
    }

    public enum UserType
    {
        Admin = 1,
        Author = 2,
        Member = 3,
        Prospect = 4
    }

    [Flags]
    public enum UserFlagsBL
    {
        Default = 0,
        Blocked = 1,
        NotConfirmed = 2
    }
}
