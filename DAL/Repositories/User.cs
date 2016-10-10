using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        List<User> GetAllUsers();
        User GetUserByEmailPass(string email, string pass);
        int InsertUser(User user);
        User GetUserByEmail(string email);
    }

    public partial class Repository : IUserRepository
    {
        #region Get

        public User GetUserById(int id)
        {
            var user = new User();

            _dbRead.Execute(
                "UserGetById",
            new[] { 
                new SqlParameter("@Id", id), 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlags>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                });

            if (user.Id == 0)
            {
                return null;
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = new User();

            _dbRead.Execute(
                "UserGetByEmail",
            new[] { 
                new SqlParameter("@Email", email), 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlags>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                });

            return user;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            _dbRead.Execute(
                "UserGetAll",
            null,
                r => users.Add(new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlags>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                }));

            return users;
        }

        public User GetUserByEmailPass(string email, string pass)
        {
            var user = new User();

            _dbRead.Execute(
                "UserGetByNamePass",
            new[] { 
                new SqlParameter("@Password", pass), 
                new SqlParameter("@Email", email) 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlags>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                });

            return user;
        }

        #endregion

        #region Insert

        public int InsertUser(User user)
        {
            int userId = 0;
            _dbRead.Execute(
                "UserInsert",
            new[] { 
                new SqlParameter("@FirstName", user.FirstName), 
                new SqlParameter("@LastName", user.LastName), 
                new SqlParameter("@JoinDate", user.JoinDate), 
                new SqlParameter("@Flags", user.Flags), 
                new SqlParameter("@Email", user.Email), 
                new SqlParameter("@PasswordHashed", user.PasswordHashed), 
                new SqlParameter("@Id_typ", user.UserType), 
            },
                r =>
                userId = Read<int>(r, "Id")
            );
            return userId;
        }

        #endregion
    }
}
