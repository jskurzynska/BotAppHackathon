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
        private readonly QuestionProvider _techProvider;
        private readonly  TechnicalQuestions _technicalQuestions = new TechnicalQuestions();

        

        private int _asked_questions_count;
        public bool AllowToNextQuestion { get; set; }
        public Topic CurrentTopic { get; set; }
        public float Sentiment { get; set; }

        public QuestionsManager()
        {
            _questionsProvider = new QuestionProvider();
            _techProvider = new QuestionProvider();
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

            if (_asked_questions_count < 1)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Introduction);
                CurrentTopic = Topic.Introduction;
            }
            else if (_asked_questions_count == 2)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Closer);
                CurrentTopic = Topic.Closer;
            }
            else if (_asked_questions_count == 3)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Casual);
                CurrentTopic = Topic.Casual;
            }
            else if (_asked_questions_count == 4)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Experience);
                CurrentTopic = Topic.Experience;
            }
            else
            {
                question = _technicalQuestions.GetRandomQuestion(Topic.Introduction);
                CurrentTopic = Topic.Technology;

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