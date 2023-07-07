using Microsoft.AspNetCore.Mvc;
using WebApplication4.Database;

namespace WebApplication4.Controllers
{
    [Produces("application/json")]
    [Route("api/Notices")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepositoryAsync _repository;
        private readonly ILogger _logger;


        public NoticesController(INoticeRepositoryAsync repository, ILoggerFactory loggerFactory)
        {
            // 데이터베이스 리파지토리 선언
            this._repository = repository;
            // 로그 선언
            this._logger = loggerFactory.CreateLogger(nameof(NoticesController)); 
        }

        // 출력
        // GET api/Notices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notices = await _repository.GetAllAsync();
                return Ok(notices); // 200 OK 와 함께 값을 반환해준다.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(); // 400 BadRequest
            }
        }

    }
}
