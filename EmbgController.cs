using Microsoft.AspNetCore.Mvc;

namespace EmbgValidatorApi.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class EmbgController : ControllerBase
    {
        private readonly EmbgValidatorService _validatorService;

        public EmbgController(EmbgValidatorService validatorService)
        {
            _validatorService = validatorService;
        }

        [HttpPost("validate")]
        public IActionResult ValidateEmbg([FromBody] string embg)
        {
            bool isValid = _validatorService.ValidateEmbg(embg);
            return Ok(new { isValid = isValid });
        }
    }
}