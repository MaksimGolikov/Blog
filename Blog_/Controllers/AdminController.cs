using Blog_.Models;
using Blog_.Models.DataBase;
using Blog_.Models.DataBase.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_.Controllers
{
    public class AdminController : Controller
    {
        static private BlogContext DBconnect;
        static private ControlTopicTable topicControl;
        static private UserFunction userControl;
        static private MessageFunction messageControl;



      
        public ActionResult Index()
        {
            DBconnect = new BlogContext();
            topicControl = new ControlTopicTable(DBconnect);
            userControl = new UserFunction(DBconnect);
            messageControl = new MessageFunction(DBconnect);

            return View("Index");
        }
        [HttpGet]
      
        public ActionResult Create()
        {
            return View("CreateTopic",new Topic());
        }       
               
        [HttpPost]
        public ActionResult Create(Topic id)
        {
            try
            {
                topicControl.AddNewTopic(id);
                return RedirectToAction("Index");                
            }
            catch
            {
                return View();
            }
        }


      
      
        public ActionResult Edit()
        {
            Tuple<IEnumerable<Topic>, Topic> select =
               new Tuple<IEnumerable<Topic>, Topic>(topicControl.GetAllTopic(), null);
            return View("EditTopic", select);
        }
              
        [HttpPost]
        public ActionResult Edit(Topic t)
        {
            topicControl.ChangeTopic(t);
            return RedirectToAction("Index");
           
        }
        public ActionResult SetData(int id)
        {
            Tuple<IEnumerable<Topic>, Topic> select = 
                new Tuple<IEnumerable<Topic>, Topic>(topicControl.GetAllTopic(),topicControl.GetTopicContext(id));

            return View("EditTopic", select);
        }
       
        [HttpGet]
        public ActionResult Delete()
        {
            return View("DeleteTopic", topicControl.GetAllTopic());
        }
        
        public ActionResult Del(int id)
        {
            topicControl.DeleteTopic(id);
            return View("DeleteTopic", topicControl.GetAllTopic());            
        }


    }
}
