using System.Reflection;
using API.Models;
using Core.Domain.Handlers;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Infra.CrossCutting;

namespace API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Mongo database
            services.AddSingleton<IMongoClient>(mongo => new MongoClient(configuration["mongoConnection:server"]));
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Domain
            services.RegisterServices(configuration);
        }
    }
}