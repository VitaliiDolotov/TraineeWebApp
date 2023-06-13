using RazorPageDemo.BL.Base;

namespace RazorPageDemo.BL.Services
{
	public interface IMapperService : IService
	{
		TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class;
		TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TSource : class where TDestination : class;
	}
}
