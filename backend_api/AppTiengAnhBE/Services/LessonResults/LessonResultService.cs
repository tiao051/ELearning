using AppTiengAnhBE.Models.DTOs.LessonDTO;
using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;
using AppTiengAnhBE.Models.DTOs.UserLessonResultDTO;
using AppTiengAnhBE.Repositories.LessonResults;

namespace AppTiengAnhBE.Services.LessonResults
{
    public class LessonResultService : ILessonResultService
    {
        private readonly ILessonResultRepository _repo;

        public LessonResultService(ILessonResultRepository repo)
        {
            _repo = repo;
        }
        public async Task<SubmitResult> ProcessSubmissionAsync(SubmitRequest request)
        {
            int totalCorrect = 0;
            int totalQuestions = request.Answers.Count;

            var correctness = new List<(int QuestionId, string AnswerText, bool IsCorrect)>();

            foreach (var ans in request.Answers)
            {
                var correctAnswers = await _repo.GetCorrectAnswersAsync(ans.QuestionId);
                bool isCorrect = correctAnswers.Contains(ans.AnswerText.Trim().ToLower());
                if (isCorrect) totalCorrect++;

                correctness.Add((ans.QuestionId, ans.AnswerText, isCorrect));
            }

            float score = totalQuestions == 0 ? 0 : (float)totalCorrect / totalQuestions * 10;
            int resultId = await _repo.CreateUserLessonResultAsync(request.UserId, request.LessonId, totalQuestions, totalCorrect, score);

            foreach (var item in correctness)
            {
                await _repo.SaveUserAnswerAsync(resultId, item.QuestionId, item.AnswerText, item.IsCorrect);
            }

            return new SubmitResult
            {
                TotalQuestions = totalQuestions,
                TotalCorrect = totalCorrect,
                Score = score,
                SubmittedAt = DateTime.Now
            };
        }
        public async Task<IEnumerable<UserExerciseAnswer>> GetAnswersByResultIdAsync(int resultId)
        {
            return await _repo.GetUserAnswersByResultIdAsync(resultId);
        }
        public async Task<IEnumerable<UserLessonResult>> GetLessonResultsByUserIdAsync(int userId)
        {
            return await _repo.GetLessonResultsByUserIdAsync(userId);
        }
    }
}
