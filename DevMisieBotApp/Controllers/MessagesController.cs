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
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        private static  readonly QuestionsManager _question_manager = new QuestionsManager();
        private static readonly KeyWordsManager _keyWords_manager = new KeyWordsManager();
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = null;

                var keyPhrases = await TextAnalytics.GetKeyPhrases(activity.Text,activity.Id);

                var message = new MessageModel()
                {
                    KeyPhrases = keyPhrases.documents[0].keyPhrases,
                    MessageID = activity.Id,
                    Text = activity.Text
                };


                MessageDB.MessagesList.Add(message);
                var keys = _keyWords_manager.FindKeyWords(activity.Text,_question_manager.CurrentTopic);
                if (keys.Count == 0)
                {
                    var topic = _keyWords_manager.RecognizeTopic(activity.Text);
                    if (topic != Topic.None)
                    {
                        reply = activity.CreateReply(_question_manager.GetQuestion(topic));
                    }
                }
                _question_manager.AllowToNextQuestion = keys.Count > 0;
                if (reply == null)
                {
                    reply = activity.CreateReply(_question_manager.GetQuestion());
                }

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
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