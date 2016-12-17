using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DevMisieBotApp.Models;
using Microsoft.Bot.Connector;
using DevMisieBotApp.DB;
using DevMisieBotApp.Services;
using DevMisieBotApp.Questions;


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
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                var keyPhrases = await TextAnalytics.GetKeyPhrases(activity.Text,activity.Id);
                var message = new MessageModel()
                {
                    KeyPhrases = keyPhrases.documents[0].keyPhrases,
                    MessageID = activity.Id,
                    Text = activity.Text
                };
                MessageDB.MessagesList.Add(message);

               // Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                //var reply = activity.CreateReply(_casualQuestions.GetRandomQuestion());
                var proper_answer = _question_manager.GetAnswerPersentage(activity.Text);
                var reply = activity.CreateReply(_question_manager.GetQuestion());
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