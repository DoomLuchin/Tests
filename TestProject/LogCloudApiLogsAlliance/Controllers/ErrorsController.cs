using CommonsResources.Helpers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LogsAllianceApi.Controllers
{
    [Route("errors/{code}")]
    public class ErrorsController : BaseApiController
    {
        [HttpGet]
        [HttpPost]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
