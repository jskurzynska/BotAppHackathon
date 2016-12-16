using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevMisieBotApp.Common;

namespace DevMisieBotApp.Questions
{
    public class QuestionProvider:IQuestionsProvider
    {
        private readonly List<string> _questions;

        public QuestionProvider(List<string>  question)
        {
            _questions = question;
        }
        private List<string> _used = new List<string>();
        public string GetRandomQuestion()
        {
            if (_used.Count == _questions.Count)
            {
                _used.Clear();
            }
            var random_question = _questions.Where(x => !_used.Contains(x)).Random();
            _used.Add(random_question);
            return random_question;
        }
    }
}