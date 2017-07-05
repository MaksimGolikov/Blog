using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_.Models.DataBase.Function
{
    public class UserFunction
    {

        private BlogContext DBconnect;

        public UserFunction(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }



        public User GetUserIfExist(User user)
        {
            var retrnedValue = DBconnect.Users.Where(u => u.Login== user.Login && u.Password == user.Password).FirstOrDefault();
            return retrnedValue;
        }
        public User FindUserByLogin(string login)
        {
            var retrnedValue = DBconnect.Users.Where(u => u.Login == login).FirstOrDefault();
            return retrnedValue;
        }

        public User GetEmpty()
        {
            var guest = new User();
            return guest;
        }

        public void CreateNewUser(User user)
        {
            user.Role = "user";
            DBconnect.Users.Add(user);
            DBconnect.SaveChanges();
        }


    }
}