using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IDataRepository _repository;

        public AddressController(IDataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Address
        [Authorize]
        [HttpGet]
        public IEnumerable<Address> Get()
            => _repository.GetAllAddresses();

        // GET: api/Address/{id}
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var address = _repository.GetAddress(id);
            if (address is null) return NotFound();
            return Ok(address);
        }

        // POST: api/Address
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Address address)
        {
            _repository.AddAddress(address);
            return Ok(address);
        }

        // DELETE: api/Address/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var address = _repository.GetAddress(id);
            if (address is null) return NotFound();

            _repository.DeleteAddress(address);
            return Ok();
        }
    }
}