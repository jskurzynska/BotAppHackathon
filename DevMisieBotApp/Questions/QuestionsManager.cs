using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevMisieBotApp.Conversation;

namespace DevMisieBotApp.Questions
{
    public class QuestionsManager
    {
        private const int HELLO_SENTECE = 1;
        private const int CLOSER = 2;
        private const int MAX_CASUAL_QUESTIONS = 3;
        private const int TOJOB = 4;
        private readonly QuestionProvider _questionsProvider;
        

        private int _asked_questions_count;
        public bool AllowToNextQuestion { get; set; }
        public Topic CurrentTopic { get; set; }
        public float Sentiment { get; set; }

        public QuestionsManager()
        {
            _questionsProvider = new QuestionProvider();
            Sentiment = 0.5f;
            AllowToNextQuestion = true;
            _asked_questions_count = 0;
            CurrentTopic = Topic.Introduction;
        }


        public string GetQuestion(Topic topic)
        {
            string answer = "";
            switch (topic)
            {
                    case Topic.Joke:
                {
                    answer = _questionsProvider.GetRandomQuestion(Topic.Joke);
                    break;
                }
                case Topic.Reset:
                {
                    answer = _questionsProvider.GetRandomQuestion(Topic.Reset);
                    Reset();
                    break;
                }
            }
            return answer;
        }

        public string GetQuestion()
        {
            string question;

            if (_asked_questions_count < HELLO_SENTECE)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Introduction);
                CurrentTopic = Topic.Introduction;
            }
            else if (_asked_questions_count < CLOSER)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Closer);
                CurrentTopic = Topic.Closer;
            }
            else if (_asked_questions_count < MAX_CASUAL_QUESTIONS)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Experience);
                CurrentTopic = Topic.Casual;
            }
            else
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Technology);
                CurrentTopic = Topic.Joke;

            }
            _asked_questions_count++;
            return question;
        }

        public void Reset()
        {
            AllowToNextQuestion = true;
            CurrentTopic = Topic.Introduction;
            _asked_questions_count = 0;
        }

    }
}