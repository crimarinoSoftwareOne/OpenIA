namespace MsOpenIA.Controllers
{
    using MsOpenIA.DTO;
    using MsOpenIA.Handlers;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class OpenIaController : ControllerBase
    {
        private readonly HandlerService _handlerService;

        public OpenIaController(HandlerService handlerService) =>
            _handlerService = handlerService;

        [HttpPost(nameof(GetAll))]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> GetAll([FromBody] ResponseDto model) =>
            ProcessRequest(model, _handlerService.GetAllAsync);

        [HttpPost(nameof(Save))]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> Save([FromBody] ResponseDto model) =>
            ProcessRequest(model, _handlerService.SaveAsync);

        [HttpPost(nameof(Get))]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> Get([FromBody] ResponseDto model) =>
            ProcessRequest(model, _handlerService.GetAsync);

        [HttpPost(nameof(Tokenizer))]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> Tokenizer([FromBody] ResponseDto model) =>
            ProcessRequest(model, _handlerService.TokenizerAsync);

        [HttpPost(nameof(Analyzer))]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> Analyzer([FromBody] ResponseDto model) =>
            ProcessRequest(model, _handlerService.AnalyzerAsync);

        private async Task<IActionResult> ProcessRequest(ResponseDto model, Func<ResponseDto, Task<ResponseDto>> serviceMethod)
        {
            try
            {
                return Ok(await serviceMethod(model));
            }
            catch (Exception ex)
            {
                var errorResponse = await _handlerService.HandleError(ex);
                return StatusCode((int)errorResponse.Status, errorResponse);
            }
        }
    }
}