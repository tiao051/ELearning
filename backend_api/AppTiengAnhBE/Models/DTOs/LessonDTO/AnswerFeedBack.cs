namespace AppTiengAnhBE.Models.DTOs.LessonDTO
{
    public class AnswerFeedback
    {
        public int QuestionId { get; set; }
        public required string YourAnswer { get; set; }
        public required string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
