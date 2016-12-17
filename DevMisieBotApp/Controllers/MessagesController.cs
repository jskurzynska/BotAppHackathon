using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DevMisieBotApp.Conversation;
using DevMisieBotApp.Models;
using Microsoft.Bot.Connector;
using DevMisieBotApp.DB;
using DevMisieBotApp.Services;
using DevMisieBotApp.Questions;
using TTSSample;


namespace DevMisieBotApp
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        private static  readonly QuestionsManager _question_manager = new QuestionsManager();
        private static readonly KeyWordsManager _keyWords_manager = new KeyWordsManager();
        [HttpPost]
        public IHttpActionResult Post([FromBody]Activity activity)
        {
            if (string.IsNullOrEmpty(activity.Text))
            {
                return NotFound();
            }

            string reply = "";
            if (activity.Type == ActivityTypes.Message)
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                var keyPhrases =  TextAnalytics.GetKeyPhrases(activity.Text,activity.Id);
              
                //MessageDB.MessagesList.Add(message);
                var keys = _keyWords_manager.FindKeyWords(activity.Text,_question_manager.CurrentTopic);
                if (keys.Count == 0)
                {
                    var topic = _keyWords_manager.RecognizeTopic(activity.Text);
                    if (topic != Topic.None)
                    {
                        reply = _question_manager.GetQuestion(topic);
                    }
                }
                _question_manager.AllowToNextQuestion = keys.Count > 0;
                if (String.IsNullOrEmpty(reply))
                {
                    if (_question_manager.CurrentTopic == Topic.Experience)
                    {
                        if (_question_manager.CurrentTopic == Topic.Experience)
                        {
                            string txt = "";
                            foreach (var key in keyPhrases.Result.documents)
                            {
                                txt += key + ", ";
                            }
                            txt += " seems cool!";
                        }
                    }
                    reply = _question_manager.GetQuestion();
                }

                // connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //response.Content = 
            //return response;
            return Ok(reply);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}