using AutoMapper;
using Food.Dtos;
using Food.Model;

namespace Food.Profiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order,OrderRegisterDto>();
			CreateMap<OrderRegisterDto,Order>();
		}
	}
}
