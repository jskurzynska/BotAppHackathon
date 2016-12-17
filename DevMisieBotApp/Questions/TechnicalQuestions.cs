using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevMisieBotApp.Common;
using DevMisieBotApp.Conversation;

namespace DevMisieBotApp.Questions
{
    public class TechnicalQuestions : IQuestionsProvider
    {
        public const float NOT_QUESTION_ASKED = -1;
        private Dictionary<string, List<string>> _questions = new Dictionary<string, List<string>>()
        {
            [@"What do you know about ""const"" keyword in .NET"] = new List<string>() { "compilation", "must", "assign", },
            ["What is Boxing and Unboxing?"] = new List<string>() { "conversion", "type", },
            ["Can “this” be used within a static method?"] = new List<string>() { "no","nope", },
            ["What is extension method in c# and how to use them?"] = new List<string>() { "recompiling", "recompile", "modifying","satic","this","class","method" },
            ["What is delegates in C#?"] = new List<string>() { "reference", "method", "pointer","removed","assign"},
            ["What is sealed class in c#?"] = new List<string>() { "restrict ", "cannot", "inheritance ", "inherit", "struct" },
            ["What are partial classes?"] = new List<string>() { "splits", "multiple", "files ", "namespace" },
            ["How to use Nullable<> Types in .Net?"] = new List<string>() { "?", "keyword", "value ", "types" },
            ["What are generics in c#.net?"] = new List<string>() { "List", "Dictionary", "Queue ", "Stack","Concurrent" },
            ["Describe the accessibility modifiers in c#.Net."] = new List<string>() { "public", "private", "prtected ", "internal","protected internal" },
            ["What is the use of Using statement in C#?"] = new List<string>() { "namespace", "IDisposable", "static", "include" },

        };

        private List<string> _used = new List<string>();
        private string _lastQuestion;
        public string GetRandomQuestion(Topic txt)
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
            return correct_key_words / key_words.Count * 100;
        }
    }
}