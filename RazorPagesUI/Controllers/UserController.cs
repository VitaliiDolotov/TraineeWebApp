using Microsoft.AspNetCore.Mvc;
using RazorPageDemo.BL.Services;
using RazorPageDemo.Services;
using RazorPagesDemo.Models.DTO;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IDataRepository _repository;
		private readonly IMapperService _mapper;

		public UserController(IDataRepository repository, IMapperService mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		// GET: api/<UserController>
		[HttpGet]
		public IEnumerable<UserDtoResponse> Get()
		{
			var users = _repository.GetAllUsers();
			var responseUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserDtoResponse>>(users);
			return responseUsers;
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var user = _repository.GetUser(id);

			if (user is null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<User, UserDtoResponse>(user));
		}

		// POST api/<UserController>
		[HttpPost]
		public IActionResult Post(UserDtoRequest userRequest)
		{
			var user = _mapper.Map<UserDtoRequest, User>(userRequest);

			_repository.AddUser(user);

			var responseUser = _mapper.Map<User, UserDtoResponse>(user);

			return Ok(responseUser);
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var user = _repository.GetUser(id);

			if (user is null)
			{
				return NotFound();
			}

			_repository.DeleteUser(user);

			return Ok();
		}
	}
}
