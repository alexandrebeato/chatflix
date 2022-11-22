using CommandStack.Rooms.Commands;
using Domain.Rooms;

namespace CommandStack.Rooms.Adapters
{
    public static class RoomAdapter
    {
        public static Room ToRoom(this CreateRoomCommand command) =>
            Room.Factory.CreateInstance(command.Id, command.Name, command.CreatedAt);
    }
}