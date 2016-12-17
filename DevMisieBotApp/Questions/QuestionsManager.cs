using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevMisieBotApp.Questions
{
    public class QuestionsManager
    {
        private const int HELLO_SENTECE = 1;
        private const int MAX_CASUAL_QUESTIONS = 5 ;
        private const int JOB_POSITION_QUESTION = 5 + MAX_CASUAL_QUESTIONS;
        private readonly QuestionProvider _casual_questions;
        private readonly QuestionProvider _job_related_questions;
        private readonly TechnicalQuestions _technical_questions = new TechnicalQuestions();

        private int _asked_questions_count;

        public QuestionsManager()
        {
            var casual_questions = new CasualQuestions();
            _casual_questions = new QuestionProvider(casual_questions._questions);

            var job_related_questions = new QuestionAboutJobPosition();
            _job_related_questions = new QuestionProvider(job_related_questions._questions);
        }

        public string GetQuestion()
        {
            string question;
            if (_asked_questions_count < HELLO_SENTECE)
            {
                question = "Hello! What's your name and surname?";
            }
            else if (_asked_questions_count < MAX_CASUAL_QUESTIONS)
            {
                question =  _casual_questions.GetRandomQuestion();
            }
            else if (_asked_questions_count < JOB_POSITION_QUESTION)
            {
                question = _technical_questions.GetRandomQuestion();
            }
            else
            {
                question = _job_related_questions.GetRandomQuestion();
            }
            _asked_questions_count++;
            return question;
        }

        public float GetAnswerPersentage(string activityText)
        {
            if (_asked_questions_count < MAX_CASUAL_QUESTIONS)
            {
                return 0;
            }
            return _technical_questions.GetAnswerPersentage(activityText);
        }
    }
}