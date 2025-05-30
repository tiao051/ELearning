using AppTiengAnhBE.Services.QuestionServices;
using Microsoft.AspNetCore.Mvc;

namespace AppTiengAnhBE.Controllers.QuestionsControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("lesson/{lessonId}")]
        public async Task<IActionResult> GetQuestionsByLesson(int lessonId)
        {
            var questions = await _questionService.GetQuestionsByLessonAsync(lessonId);
            return Ok(questions);
        }
    }
}
