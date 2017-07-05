using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_.Models.DataBase.Function
{
    public class MessageFunction
    {

        private BlogContext DBconnect;


        public MessageFunction(BlogContext newDBconnect)
        {
            DBconnect = newDBconnect;
        }


        public IEnumerable<Message> GetMessageByTopic(int idTopic)
        {
            var retrnedValue = DBconnect.Messages.Where(m => m.IdTopic == idTopic);
            return retrnedValue;
        }

        public void AddMessage(Message message)
        {
            DBconnect.Messages.Add(message);
            DBconnect.SaveChanges();
        }



    }
}