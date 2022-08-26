using Demo1.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly INameService _nameService;

        public NameController(INameService nameService)
        {
            _nameService = nameService;
        }

        [HttpGet]
        [Route("alive")]
        public IActionResult Alive()
        {
            return Content("Api Alive");
        }

        [HttpGet]
        public StatusCodeResult Get(string name)
        {
            var isValidName = _nameService.isValidName(name);
            if (isValidName)
                return Ok();
            else
                return BadRequest();
        }
    }
}
