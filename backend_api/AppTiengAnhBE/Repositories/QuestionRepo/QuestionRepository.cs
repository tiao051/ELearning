using AppTiengAnhBE.Models.DTOs.QuestionDTO;
using Dapper;
using System.Data;

namespace AppTiengAnhBE.Repositories.QuestionRepo
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbConnection _db;

        public QuestionRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsByLessonAsync(int lessonId)
        {
            var sql = @"
            SELECT q.id, q.question AS QuestionText, qt.type_name AS TypeName
            FROM question q
            JOIN question_types qt ON q.question_type_id = qt.id
            WHERE q.lesson_id = @LessonId;

            SELECT qa.id, qa.question_id AS QuestionId, qa.answer_text AS AnswerText, qa.is_correct
            FROM question_answers qa
            JOIN question q ON qa.question_id = q.id
            WHERE q.lesson_id = @LessonId;";


            using var multi = await _db.QueryMultipleAsync(sql, new { LessonId = lessonId });

            var questions = (await multi.ReadAsync<QuestionDTO>()).ToList();
            var answers = (await multi.ReadAsync<AnswerDTO>()).ToList();

            foreach (var q in questions)
            {
                q.AnswersText = answers.Where(a => a.QuestionId == q.Id).ToList();
            }

            Console.WriteLine($"Fetched {questions.Count} questions");
            Console.WriteLine($"Fetched {answers.Count} answers");

            foreach (var a in answers)
            {
                Console.WriteLine($"Answer ID: {a.Id}, QID: {a.QuestionId}, Text: {a.AnswerText}");
            }

            return questions;
        }
    }
}
