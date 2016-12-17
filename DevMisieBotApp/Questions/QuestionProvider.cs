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
            "Welcome in our recruitment process, I'm your personal assistant at this stage! I can see you, recognize your sentiment and gather all needed information!"
            }, Topic.Introduction);
            AddQuestionsToPool(new List<string>
            {
            "So, really nice to meet you! How are you feeling, can we meet closer? ",
            "You seems really positive person, can we meet closer?",
            }, Topic.Closer);

            AddQuestionsToPool(new List<string>
            {
            "Let's start with the easy one. If you could travel anywhere, where would you go and why?",
            "What kind of music do you like listen to?"
            }, Topic.Casual);

            AddQuestionsToPool(new List<string>
            {
             "I didn't get it, can u repeat?",
             "Can u talk a bit slower? Thanks so much, I'm still learning!"
            }, Topic.Attempt);
            AddQuestionsToPool(new List<string>
            {
             "Do you think maybe about change your current position?"
            }, Topic.Job);
            AddQuestionsToPool(new List<string>
            {
             "Tell me more about your experience!",
             "Why do you want this job?",
             "What are your future plans for this job?",
             "What is the key to success in this organization?",
             "What Is Your Greatest Strength?",
             "Do You Have Any Questions?",
             "If you knew a manager is 100 percent wrong about something, how would you handle it?",
             "What are your salary expectations?",
             "How would you feel supervising two or three other employees?",
             "Are you willing to relocate?",
             "What are you looking for in terms of career development?",
             "What do you ultimately want to become?",
             "What's the last book you read?",
             "What do you know about this industry?"
            }, Topic.Experience);
            AddQuestionsToPool(new List<string>
            {
             ""
            }, Topic.Joke);
            AddQuestionsToPool(new List<string>
            {
             "We can start from scratch :) Say hello to start."
            }, Topic.Reset);
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