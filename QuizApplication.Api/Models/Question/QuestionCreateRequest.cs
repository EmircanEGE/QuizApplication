namespace QuizApplication.Api.Models.Question
{
    public class QuestionCreateRequest
    {
        public string Text { get; set; }
        public int QuizId { get; set; }
    }
}
