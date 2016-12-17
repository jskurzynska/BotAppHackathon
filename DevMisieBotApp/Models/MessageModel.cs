using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevMisieBotApp.Models
{
    public class MessageModel
    {
        public string Text { get; set; }
        public string MessageID { get; set; }
        public  string[] KeyPhrases { get; set; }
        public float Sentiment { get; set; }
    }
}