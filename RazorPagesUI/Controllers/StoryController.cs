using Microsoft.AspNetCore.Mvc;
using RazorPageDemo.BL.Services;
using RazorPageDemo.Services;
using RazorPageDemo.Services.Models;
using RazorPagesDemo.Models.Response;

namespace RazorPagesUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoryController : ControllerBase
	{
		private readonly IDataRepository _repository;
		private readonly IMapperService _mapper;

		public StoryController(IDataRepository repository, IMapperService mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		// GET: api/<StoryController>?language=GB
		[HttpGet]
		public IActionResult Get([FromQuery] Language language)
		{
			var stories = _repository.GetAllStories();
			var responseStories = _mapper
				.Map<IEnumerable<Story>, IEnumerable<StoryResponseDto>>(stories, opts =>
				{
					opts.Items["Language"] = language;
				});

			return Ok(responseStories);
		}

		[HttpGet("{id}")]
		public IActionResult GetStoryByIdAndLanguage(int id, [FromQuery] Language language)
		{
			var story = _repository.GetStoryById(id);

			if (story is null)
			{
				return NotFound();
			}

			var storyDetailsDto = _mapper
				.Map<Story, StoryDetailsDto>(story, opts => {
				opts.Items["Language"] = language;
			});

			return Ok(storyDetailsDto);
		}
	}
}
