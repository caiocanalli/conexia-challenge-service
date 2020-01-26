using AutoMapper;
using Conexia.Challenge.Application.Responses;
using Conexia.Challenge.Domain.Users;

namespace Conexia.Challenge.Application.Users.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RecoverByIdResponse>();
        }
    }
}