using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;

namespace AppTiengAnhBE.Repositories.UserQuestionAnswers
{
    public interface IUserQuestionAnswerRepository
    {
        Task<IEnumerable<UserAnswerDetail>> GetUserAnswerDetailsByResultIdAsync(int userResultId);
    }
}
