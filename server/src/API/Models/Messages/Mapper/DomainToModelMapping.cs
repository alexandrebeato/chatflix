using AutoMapper;
using Domain.Messages;
using Domain.Messages.ValueObjects;

namespace API.Models.Messages.Mapper
{
    public class DomainToModelMapping : Profile
    {
        public DomainToModelMapping()
        {
            CreateMap<Message, MessageModel>();
            CreateMap<User, UserModel>();
        }
    }
}