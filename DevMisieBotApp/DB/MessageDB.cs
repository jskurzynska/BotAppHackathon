using System.Collections.Generic;
using DevMisieBotApp.Models;

namespace DevMisieBotApp.DB
{
    public static class MessageDB
    {
        public static List<MessageModel> MessagesList { get; set; }

        static MessageDB()
        {
            MessagesList = new List<MessageModel>();
        }

        public static void AddMessage(MessageModel model)
        {
            MessagesList.Add(model);
        }

    }
}