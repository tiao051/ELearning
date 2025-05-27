using AppTiengAnhBE.Models.DTOs.LessonDTO;
using AppTiengAnhBE.Services.LessonResults;
using AppTiengAnhBE.Services.UserQuestionAnswers;
using Microsoft.AspNetCore.Mvc;

namespace AppTiengAnhBE.Controllers.LessonsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonResultsController : ControllerBase
    {
        private readonly ILessonResultService _lessonResultService;
        private readonly IUserQuestionAnswerService _userQuestionAnswerService;

        public LessonResultsController
            (ILessonResultService lessonResultService,
            IUserQuestionAnswerService userQuestionAnswerService)
        {
            _lessonResultService = lessonResultService;
            _userQuestionAnswerService = userQuestionAnswerService;
        }

        [HttpPost("submit")]
        public async Task<ActionResult<SubmitResult>> SubmitTest([FromBody] SubmitRequest request)
        {
            var result = await _lessonResultService.ProcessSubmissionAsync(request);
            return Ok(result);
        }

        [HttpGet("answers/{resultId}")]
        public async Task<IActionResult> GetUserAnswers(int resultId)
        {
            var answers = await _lessonResultService.GetAnswersByResultIdAsync(resultId);
            if (answers == null || !answers.Any())
            {
                return NotFound($"No answers found for resultId = {resultId}");
            }
            return Ok(answers);
        }

        [HttpGet("answers/details/{resultId}")]
        public async Task<IActionResult> GetUserAnswerDetails(int resultId)
        {
            var details = await _userQuestionAnswerService.GetUserAnswerDetailsAsync(resultId);

            if (details == null || !details.Any())
                return NotFound($"No answers found for resultId = {resultId}");

            return Ok(details);
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetUserLessonHistory(int userId)
        {
            var results = await _lessonResultService.GetLessonResultsByUserIdAsync(userId);
            if (results == null || !results.Any())
            {
                return NotFound($"No lesson results found for userId = {userId}");
            }
            return Ok(results);
        }
    }
}
