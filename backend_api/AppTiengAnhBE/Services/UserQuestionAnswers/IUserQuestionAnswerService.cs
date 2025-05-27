using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;

namespace AppTiengAnhBE.Services.UserQuestionAnswers
{
    public interface IUserQuestionAnswerService
    {
        Task<IEnumerable<UserAnswerDetail>> GetUserAnswerDetailsAsync(int userResultId);
    }
}
