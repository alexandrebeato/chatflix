using CommandStack.Messages.Commands;
using CommandStack.Messages.Events;
using CommandStack.Messages.Handlers;
using CommandStack.Rooms.Commands;
using CommandStack.Rooms.Handlers;
using CommandStack.Users.Commands;
using CommandStack.Users.Handlers;
using Domain.Messages.Repository;
using Domain.Rooms.Repository;
using Domain.Users.Repository;
using Infra.Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting
{
    public static class Bootstrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            // Command handlers
            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateRoomCommand, bool>, RoomCommandHandler>();
            services.AddScoped<IRequestHandler<CreateMessageCommand, bool>, MessageCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteMessageCommand, bool>, MessageCommandHandler>();

            // Event handlers
            services.AddScoped<INotificationHandler<MessageCreatedEvent>, MessageEventHandler>();
        }
    }
}