using AutoMapper;
using CommandStack.Rooms.Commands;

namespace API.Models.Rooms.Mapper
{
    public class ModelToDomainMapping : Profile
    {
        public ModelToDomainMapping()
        {
            CreateMap<CreateRoomModel, CreateRoomCommand>()
                .ConstructUsing(c => new CreateRoomCommand(c.Id, c.Name, c.CreatedAt));
        }
    }
}