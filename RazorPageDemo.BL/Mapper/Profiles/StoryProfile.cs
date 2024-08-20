using AutoMapper;
using RazorPageDemo.Services.Models;
using RazorPagesDemo.Models.Response;

namespace RazorPageDemo.BL.Mapper.Profiles
{
	public class StoryProfile : Profile
	{
		public StoryProfile()
		{
			CreateMap<Story, StoryResponseDto>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom((src, dest, destMember, context) =>
					src.Title.ContainsKey((Language)context.Items["Language"]) ? src.Title[(Language)context.Items["Language"]] : "No title available"))
				.ForMember(dest => dest.Image, opt => opt.MapFrom((src, dest, destMember, context) =>
					src.Images.ImageUrls.Any() ? src.Images.ImageUrls[new Random().Next(src.Images.ImageUrls.Count)] : string.Empty));

			CreateMap<Story, StoryDetailsDto>()
				.ForMember(dest => dest.Text, opt => opt.MapFrom((src, dest, destMember, context) =>
					src.Texts.ContainsKey((Language)context.Items["Language"]) ? src.Texts[(Language)context.Items["Language"]] : "No text available"))
				.ForMember(dest => dest.Voice, opt => opt.MapFrom((src, dest, destMember, context) =>
					src.Voices.ContainsKey((Language)context.Items["Language"]) ? src.Voices[(Language)context.Items["Language"]] : null));
		}
	}
}
