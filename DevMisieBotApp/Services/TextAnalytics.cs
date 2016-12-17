using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevMisieBotApp.Models;
using Newtonsoft.Json;

namespace DevMisieBotApp.Services
{
    public static class TextAnalytics
    {
        public static void test()
        {
            var test = new RequestKeyPhrases();
            test.documents = new DocumentRequest[1];
            test.documents[0].text =
                "I had a wonderful experience! The rooms were wonderful and the staff were helpful.";
            test.documents[0].id = "1";
            test.documents[0].language = "en";

            // TextAnalytics.GetKeyPhrases(test);
        }
        public static async Task<ResponseSentiment> GetSentiment(string text, string id)
        {
            var request = new RequestSentiment();
            request.documents = new DocumentRequest[1];
            request.documents[0].text = text;
            request.documents[0].id = id;
            request.documents[0].language = "en";

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "28645d26ff7d46018d76b69f78804b08");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment?" + queryString;

            HttpResponseMessage response;


            response = await client.PostAsJsonAsync(uri, request);
            var body = response.Content.ReadAsStringAsync();

            ResponseSentiment output = JsonConvert.DeserializeObject<ResponseSentiment>(body.Result);
            return output;

        }

        public static async Task<ResponseKeyPhrases> GetKeyPhrases(string text,string id)
        {

            var request = new RequestKeyPhrases();
            request.documents = new DocumentRequest[1];
            request.documents[0] = new DocumentRequest();
            request.documents[0].text = text;
            request.documents[0].id = id;
            request.documents[0].language = "en";

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "28645d26ff7d46018d76b69f78804b08");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases?" + queryString;

            HttpResponseMessage response;


                response = await client.PostAsJsonAsync(uri, request);
                    var body = response.Content.ReadAsStringAsync();

           ResponseKeyPhrases output = JsonConvert.DeserializeObject<ResponseKeyPhrases>(body.Result);
            return output;
        }

    }
}