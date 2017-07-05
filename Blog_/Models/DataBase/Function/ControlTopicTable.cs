using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Blog_.Models.DataBase.Function
{
    public class ControlTopicTable
    {
        private BlogContext DBconnect;

        public ControlTopicTable(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }

        public IEnumerable<Topic> GetAllTopic()
        {
            return DBconnect.Topic;
        }
        public void AddNewTopic(Topic newTopic)
        {
            newTopic.PablishingData = DateTime.Now.ToShortDateString();
            DBconnect.Topic.Add(newTopic);
            DBconnect.SaveChanges();
        }

        public Topic GetTopicContext(int idTopic)
        {
            var context = DBconnect.Topic.Find(idTopic);
            return context;
        }

        public void ChangeTopic(Topic topic)
        {           
            topic.PablishingData = DateTime.Now.ToShortDateString();
            var local = DBconnect.Topic.Where(t=>t.Id == topic.Id).FirstOrDefault();
            local.NameTopic = topic.NameTopic;
            local.ContextTopic = topic.ContextTopic;
            local.PablishingData = topic.PablishingData;

            DBconnect.Entry(local).State = EntityState.Modified;
            DBconnect.SaveChanges();
        }

        public void DeleteTopic(int idTopic)
        {
            Topic t = DBconnect.Topic.Find(idTopic);
            DBconnect.Topic.Remove(t);
            DBconnect.SaveChanges();
        }
        

        
    }
}