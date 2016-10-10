using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public DateTime JoinDate;
        public string Email;
        public string PasswordHashed;
        public UserFlags Flags;
        public EmailPreferenceBL EmailPreference;

        public UserType UserType;
    }

    [Flags]
    public enum UserFlags
    {
        Default = 0,
        Blocked = 1,
        NotConfirmed = 2
    }

    public enum UserType
    {
        Admin = 1,
        Author = 2,
        Member = 3,
        Prospect = 4
    }

    [Flags]
    public enum EmailPreferenceBL
    {
        None = 0,
        All = 1,
        NoArticles = 2,
        NoQuestions = 4,
        NoAnswers = 8,
        NoComments = 16,
        NoNewsletter = 32,
    }
}
