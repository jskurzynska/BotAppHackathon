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
        private const int MAX_CASUAL_QUESTIONS = 2;
        private const int JOB_POSITION_QUESTION = 3 + MAX_CASUAL_QUESTIONS;
        private readonly QuestionProvider _questionsProvider;
        private readonly  TechnicalQuestions _technicalQuestions = new TechnicalQuestions();
        

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

            if (Sentiment < 0.4f)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Joke);
            }
            else if (!AllowToNextQuestion)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Attempt);
            }
            else if (_asked_questions_count < HELLO_SENTECE)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Introduction);
                CurrentTopic = Topic.Introduction;
            }
            else if (_asked_questions_count < MAX_CASUAL_QUESTIONS)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.BadAnswer);
                CurrentTopic = Topic.BadAnswer;
            }
            else if (_asked_questions_count < JOB_POSITION_QUESTION)
            {
                question = _questionsProvider.GetRandomQuestion(Topic.Experience);
                CurrentTopic = Topic.Experience;
            }
            else
            {
                question = _technicalQuestions.GetRandomQuestion(Topic.Introduction);
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