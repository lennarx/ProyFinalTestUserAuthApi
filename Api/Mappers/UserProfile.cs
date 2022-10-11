
using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Api.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
