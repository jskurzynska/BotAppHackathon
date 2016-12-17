using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevMisieBotApp.Common;
using DevMisieBotApp.Conversation;

namespace DevMisieBotApp.Questions
{
    public class QuestionProvider:IQuestionsProvider
    {
        public Dictionary<Topic,List<string>> Questions;
        public Topic Topic { get; set; }

        public QuestionProvider()
        {
            Questions = new Dictionary<Topic, List<string>>();
            AddQuestionsToPool(new List<string>
            {
             "How are you?",
            "How are you feeling?",
            "What are you going to do today?",
            "What is your best childhood memory?",
            "What do you feel most proud of?",
            "What is your favorite music?",
            "If you could travel anywhere, where would you go and why?",
            "If you could only keep five possessions, what would they be?",
            }, Topic.Joke);

            AddQuestionsToPool(new List<string>
            {
             "Try again"
            }, Topic.Attempt);

            AddQuestionsToPool(new List<string>
            {
             "Where Did you work?"
            }, Topic.Experience);
            AddQuestionsToPool(new List<string>
            {
             "Hi, hi",
             "Hello"
            }, Topic.Introduction);
            AddQuestionsToPool(new List<string>
            {
             "Where Did you work?"
            }, Topic.BadAnswer);
        }
        private Dictionary<Topic,List<string>> _used = new Dictionary<Topic, List<string>>();
        public string GetRandomQuestion(Topic topic)
        {
            List<String> questions;
            List<String> usedQuestions = null;
            string randomQuestion = "";

            if (Questions.TryGetValue(topic, out questions) && _used.TryGetValue(topic, out usedQuestions))
            {
                if (usedQuestions.Count == questions.Count)
                {
                    usedQuestions.Clear();
                }

                randomQuestion = questions.Where(x => !usedQuestions.Contains(x)).Random();
                usedQuestions.Add(randomQuestion);
            }
            return randomQuestion;
        }

        public void AddQuestionsToPool(List<string> questions, Topic topic)
        {
            List<string> currentQuestions;
            if (Questions.TryGetValue(topic, out currentQuestions))
            {
                currentQuestions.AddRange(questions);
            }
            else
            {
                Questions.Add(topic,questions);
                _used.Add(topic,new List<string>());
            }
        } 
    }
}