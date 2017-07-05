using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Blog_.Models;
using Blog_.Models.DataBase;
using Blog_.Models.Registr;
using Blog_.Models.DataBase.Function;
using System.Web.Security;

namespace Blog_.Controllers
{
    public class DatabaseController : Controller
    {
        static private BlogContext DBconnect = new BlogContext();
        static  private ControlTopicTable topicControl = new ControlTopicTable(DBconnect);
        static private UserFunction userControl = new UserFunction(DBconnect);
        static private MessageFunction messageControl = new MessageFunction(DBconnect);



        public ActionResult Index()
        {
            SetViewBag();
            return View(topicControl.GetAllTopic());
        }
        public ActionResult ShowSelected(int id)
        {
            var local = topicControl.GetTopicContext(id);
            var message = messageControl.GetMessageByTopic(id);
            Tuple<Topic, IEnumerable<Message>> model = new Tuple<Topic, IEnumerable<Message>>(local,message);

            SetViewBag();
            return View("~/Views/Database/ShowSelected.cshtml", model);
        }


        public ActionResult Me()
        {
            SetViewBag();
            return View("~/Views/AboutMe.cshtml");
        }



        #region Auntification

        public ActionResult Autification()
        {
            ViewBag.Login = false;
            return View("~/Views/Autification.cshtml");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Login(User id)
        {
            if (ModelState.IsValid)
            {               
                var user = userControl.GetUserIfExist(id);                
                if (user != null)
                {
                    var loginAccount = new Login();
                    loginAccount.LoginName = user.Login;
                    loginAccount.Password = user.Password;
                    loginAccount.Role = user.Role;
                    FormsAuthentication.SetAuthCookie(loginAccount.LoginName, true);
                    ViewBag.Login = true;
                    ViewBag.UserName = user.FirstName + " " + user.SecondName;

                    return RedirectToAction("Index");
                }              
            }
            return View("~/Views/Autification.cshtml");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Registration(User id)
        {
            if (ModelState.IsValid)
            {
                var user = userControl.GetUserIfExist(id);

                if (user ==null)
                {
                    userControl.CreateNewUser(id);
                    var u = userControl.GetUserIfExist(id);                   
                    if (user != null)
                    {
                        var loginAccount = new Login();
                        loginAccount.LoginName = u.Login;
                        loginAccount.Password = u.Password;
                        loginAccount.Role = u.Role;
                        FormsAuthentication.SetAuthCookie(loginAccount.LoginName, true);
                        ViewBag.Login = true;
                        ViewBag.UserName = user.FirstName + " " + user.SecondName;
                        return RedirectToAction("Index");
                    }
                }

            }
            return View("~/Views/Autification.cshtml");
        }
        
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            ViewBag.Login = false;          
            return RedirectToAction("Index", "Database");
        }
        #endregion

        

        public ActionResult AddMessage( Message id)
        {
            var user = userControl.FindUserByLogin(User.Identity.Name);           
            id.UserName = user.FirstName;
            messageControl.AddMessage(id);

            var local = topicControl.GetTopicContext(id.IdTopic);
            var message = messageControl.GetMessageByTopic(id.IdTopic);
            Tuple<Topic, IEnumerable<Message>> model = new Tuple<Topic, IEnumerable<Message>>(local, message);

            SetViewBag();
            return View("~/Views/Database/ShowSelected.cshtml", model);
        }


        private void SetViewBag()
        {
            ViewBag.Login = false;
            ViewBag.UserName = "Guest";
            if (User.Identity.IsAuthenticated)
            {
                var user = userControl.FindUserByLogin(User.Identity.Name);
                ViewBag.Login = true;
                ViewBag.UserName = user.FirstName;

                if (user.Role == "admin")
                {
                    ViewBag.Admin = true;
                }
            }         
               
        }

    }
}