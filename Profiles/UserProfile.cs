using AutoMapper;
using Food.Dtos;
using Food.Model;

namespace Food.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserRegisterDto>();
			CreateMap<UserRegisterDto, User>();
		}
	}
}
