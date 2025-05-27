using AppTiengAnhBE.Models.DTOs.LessonDTO;

namespace AppTiengAnhBE.Services.LessonResults
{
    public interface ILessonResultService
    {
        Task<SubmitResult> ProcessSubmissionAsync(SubmitRequest request);
        Task<IEnumerable<UserExerciseAnswer>> GetAnswersByResultIdAsync(int resultId);
    }
}
