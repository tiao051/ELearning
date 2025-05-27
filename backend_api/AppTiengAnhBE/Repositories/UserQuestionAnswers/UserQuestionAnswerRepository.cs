using AppTiengAnhBE.Models.DTOs.UserAnswerDTO;
using Dapper;
using System.Data;

namespace AppTiengAnhBE.Repositories.UserQuestionAnswers
{
    public class UserQuestionAnswerRepository : IUserQuestionAnswerRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserQuestionAnswerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserAnswerDetail>> GetUserAnswerDetailsByResultIdAsync(int userResultId)
        {
            var sql = @"
            SELECT 
                q.id AS QuestionId,
                q.question AS QuestionContent,
                qa.answer_text AS CorrectAnswerText,
                uqa.answer_text AS UserAnswerText,
                uqa.is_correct AS UserIsCorrect
            FROM user_question_answers uqa
            JOIN question q ON uqa.question_id = q.id
            LEFT JOIN question_answers qa ON qa.question_id = q.id AND qa.is_correct = true
            WHERE uqa.user_result_id = @UserResultId
            ORDER BY q.id;";

            return await _dbConnection.QueryAsync<UserAnswerDetail>(sql, new { UserResultId = userResultId });
        }
    }
}
