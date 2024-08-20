using AutoMapper;
using RazorPageDemo.BL.Mapper.Profiles;
using RazorPageDemo.BL.Services;

namespace RazorPageDemo.BL.Mapper
{
	public class MapperService : IMapperService
	{
		private readonly IMapper _mapper;

		public MapperService()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<UserProfile>();
				cfg.AddProfile<StoryProfile>();
			});

			this._mapper = config.CreateMapper();
		}

		public TDestination Map<TSource, TDestination>(TSource source)
			where TSource : class
			where TDestination : class
		{
			return _mapper.Map<TDestination>(source);
		}

		public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
			where TSource : class
			where TDestination : class
		{
			return this._mapper.Map(source, destination);
		}

		public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
			where TSource : class
			where TDestination : class
		{
			return _mapper.Map(source, default(TDestination), opts);
		}
	}
}
