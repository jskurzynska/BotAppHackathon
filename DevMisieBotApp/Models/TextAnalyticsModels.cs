
namespace DevMisieBotApp.Models
{
    public class DocumentRequest
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }

    public class DocumentResponse
    {
        public float score { get; set; }
        public string id { get; set; }
    }

    public class DocumentKeyPhrasesResponse
    {
        public string[] keyPhrases { get; set; }
        public string id { get; set; }
    }

    public class ErrorResponse
    {
        public string id { get; set; }
        public string message { get; set; }
    }

    public class RequestKeyPhrases
    {
        public DocumentRequest[] documents { get; set; }
    }

    public class RequestSentiment
    {
        public DocumentRequest[] documents { get; set; }
    }

    public class ResponseSentiment
    {
        public DocumentResponse[] documents { get; set; }
        public ErrorResponse[] errors { get; set; }
    }

    public class ResponseKeyPhrases
    {
        public DocumentKeyPhrasesResponse[] documents { get; set; }
        public ErrorResponse[] errors { get; set; }
    }
}