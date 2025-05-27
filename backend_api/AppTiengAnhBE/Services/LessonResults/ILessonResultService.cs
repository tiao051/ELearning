using AppTiengAnhBE.Models.DTOs.LessonDTO;
using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;

namespace AppTiengAnhBE.Services.LessonResults
{
    public interface ILessonResultService
    {
        Task<SubmitResult> ProcessSubmissionAsync(SubmitRequest request);
        Task<IEnumerable<UserExerciseAnswer>> GetAnswersByResultIdAsync(int resultId);
    }
}
