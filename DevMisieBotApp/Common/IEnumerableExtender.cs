using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevMisieBotApp.Common
{
    public static class IEnumerableExtender
    {
        public static string Random(this IEnumerable<string> collection)
        {
            var random = new Random().Next(0, collection.Count());
            return collection.ElementAt(random);
        }
        public static string Random(this IEnumerable<KeyValuePair<string, List<string>>> collection)
        {
            var random = new Random().Next(0, collection.Count());
            return collection.ElementAt(random).Key;
        }
    }
}