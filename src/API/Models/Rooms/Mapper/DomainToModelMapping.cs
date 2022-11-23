using AutoMapper;
using Domain.Rooms;

namespace API.Models.Rooms.Mapper
{
    public class DomainToModelMapping : Profile
    {
        public DomainToModelMapping()
        {
            CreateMap<Room, RoomModel>();
        }
    }
}