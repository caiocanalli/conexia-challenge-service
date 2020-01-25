using Conexia.Challenge.Application.Documents.Requests;
using Conexia.Challenge.Application.Documents.Responses;
using Conexia.Challenge.Application.Documents.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Conexia.Challenge.Services.Api.Controllers
{
    [ApiController]
    [Route("api/document")]
    public class DocumentController : ControllerBase
    {
        readonly IDocumentAppService _documentAppService;

        public DocumentController(IDocumentAppService documentAppService)
        {
            _documentAppService = documentAppService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromBody]UploadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _documentAppService.UploadAsync(request);

            return Ok();
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> Filter([FromQuery]FilterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            FilterResponse response =
                await _documentAppService.FilterAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _documentAppService.UpdateAsync(request);

            return Ok();
        }
    }
}