using AppTiengAnhBE.Models.DTOs.LessonDTO;
using AppTiengAnhBE.Services.LessonResults;
using Microsoft.AspNetCore.Mvc;

namespace AppTiengAnhBE.Controllers.LessonsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonResultsController : ControllerBase
    {
        private readonly ILessonResultService _lessonResultService;
        public LessonResultsController(ILessonResultService lessonResultService)
        {
            _lessonResultService = lessonResultService;
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
    }
}
