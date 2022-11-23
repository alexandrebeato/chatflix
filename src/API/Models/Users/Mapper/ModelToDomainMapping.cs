using AutoMapper;
using CommandStack.Users.Commands;

namespace API.Models.Users.Mapper
{
    public class ModelToDomainMapping : Profile
    {
        public ModelToDomainMapping()
        {
            CreateMap<CreateUserModel, CreateUserCommand>()
                .ConstructUsing(u => new CreateUserCommand(u.Id, u.UserName, u.Password, u.CreatedAt));
        }
    }
}