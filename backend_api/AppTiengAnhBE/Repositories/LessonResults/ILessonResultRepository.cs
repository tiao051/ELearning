using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;

namespace AppTiengAnhBE.Repositories.LessonResults
{
    public interface ILessonResultRepository
    {
        Task<int> CreateUserLessonResultAsync(int userId, int lessonId, int totalQuestions, int totalCorrect, float score);
        Task SaveUserAnswerAsync(int resultId, int questionId, string answerText, bool isCorrect);
        Task<List<string>> GetCorrectAnswersAsync(int questionId);
        Task<IEnumerable<UserExerciseAnswer>> GetUserAnswersByResultIdAsync(int resultId);
    }
}