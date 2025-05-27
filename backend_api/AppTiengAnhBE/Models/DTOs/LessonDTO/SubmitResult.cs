namespace AppTiengAnhBE.Models.DTOs.LessonDTO
{
    public class SubmitResult
    {
        public int TotalQuestions { get; set; }        
        public int TotalCorrect { get; set; }         
        public float Score { get; set; }              
        public DateTime SubmittedAt { get; set; }
        //public required List<AnswerFeedback> Answers { get; set; }
    }
}
