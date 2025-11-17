using Microsoft.AspNetCore.Mvc;

namespace EmbgValidatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbgController : ControllerBase
    {
        private readonly EmbgValidatorService _validatorService;

        public EmbgController(EmbgValidatorService validatorService)
        {
            _validatorService = validatorService;
        }

        [HttpPost("validate")]
        public IActionResult ValidateEmbg([FromBody] EmbgRequest request)
        {
            bool isValid = _validatorService.ValidateEmbg(request.Embg);
            return Ok(new { isValid = isValid });
        }
    }

    public class EmbgRequest
    {
        public string? Embg { get; set; }
    }
}