using AutoMapper;
using RazorPagesDemo.Models;
using RazorPagesDemo.Models.DTO;

namespace RazorPageDemo.BL.Mapper.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDtoResponse>();
			CreateMap<UserDtoRequest, User>();
		}
	}
}
