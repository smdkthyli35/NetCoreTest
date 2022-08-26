using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<CustomerDto>> GetAll()
        {
            List<CustomerDto> result = _customerService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _customerService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerDto customerDto)
        {
            _customerService.CreateNew(customerDto);
            return Created("", customerDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _customerService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CustomerDto customer)
        {
            var result = _customerService.Update(customer);
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{cityCode}")]
        public IActionResult GetByCityCode(string cityCode)
        {
            var result = _customerService.GetByCityCode(cityCode);
            return Ok(result);
        }
    }
}
