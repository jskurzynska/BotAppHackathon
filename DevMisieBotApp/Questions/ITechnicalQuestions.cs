using DevMisieBotApp.Conversation;

namespace DevMisieBotApp.Questions
{
    public interface IQuestionsProvider
    {
        string GetRandomQuestion(Topic topic);
    }
}