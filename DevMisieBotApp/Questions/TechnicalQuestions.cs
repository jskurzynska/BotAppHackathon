using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevMisieBotApp.Common;

namespace DevMisieBotApp.Questions
{
    public class TechnicalQuestions : IQuestionsProvider
    {
        public const float NOT_QUESTION_ASKED = -1;
        private Dictionary<string, List<string>> _questions = new Dictionary<string, List<string>>()
        {
            [@"What do you know about ""const"" keyword in .NET"] = new List<string>() { "compilation" },
        };

        private List<string> _used = new List<string>();
        private string _lastQuestion;
        public string GetRandomQuestion()
        {
             _lastQuestion = _questions.Where(x => !_used.Contains(x.Key)).Random();
            _used.Add(_lastQuestion);
            return _lastQuestion;
        }

        public float GetAnswerPersentage(string answer)
        {
            var words = answer.Split(' ');
            if (string.IsNullOrEmpty(_lastQuestion))
            {
                return NOT_QUESTION_ASKED;
            }
            List<string> key_words = _questions[_lastQuestion];
            int correct_key_words = 0;
            foreach (var word in words)
            {
                if (key_words.Contains(word))
                {
                    correct_key_words++;
                }
            }
           return correct_key_words/key_words.Count *100;
        }
    }
}