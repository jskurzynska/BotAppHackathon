using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevMisieBotApp.Conversation
{
    public class KeyWordsManager
    {
        private Dictionary<Topic, List<string>> words;

        public KeyWordsManager()
        {
            words = new Dictionary<Topic, List<string>>()
            {
                {Topic.Introduction, new List<string>() {"hi", "hello", "welcome", "nice to meet you"}},
                {Topic.Music, new List<string>() {"music","listening","listen", "to", "rock", "pop", "sound"}},
                {Topic.Job, new List<string>() {"not", "without", "developer", "young", "kid"}}
            };

        }

        public List<string> FindKeyWords(string text, Topic type)
        {
            text = text.ToLower();
            var keywords = new List<string>();
            MatchCollection matches = Regex.Matches(text, "[a-z]([:']?[a-z])*", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                if (words.Any(x => x.Value.Contains(match.Value) && x.Key == type))
                {
                    keywords.Add(match.Value);
                }
            }
            return keywords;
        }

        public Topic RecognizeTopic(string text)
        {
            Topic topic;
            MatchCollection matches = Regex.Matches(text, "[a-z]([:']?[a-z])*", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "joke":
                    {
                        return Topic.Joke;
                    }
                    case "reset":
                    {
                        return Topic.Reset;
                    }
                    case "restart":
                    {
                            return Topic.Reset;
                    }
                }  
            }
            return Topic.None;
        }
    }

    public class KeyWord
    {
        public string word { get; set; }
        public Topic topic { get; set; }
    }

    public enum Topic
    {
        Technology,Experience,Finance,Language,Introduction,Joke,Contact,Goodbye, Attempt, BadAnswer, GoodAnswer, None, Reset, Music,
        Job,
        Closer,
        Casual
    }

}