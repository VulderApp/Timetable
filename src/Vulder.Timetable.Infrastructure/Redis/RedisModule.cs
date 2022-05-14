using Autofac;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;
using Vulder.Timetable.Infrastructure.Redis.Repositories;

namespace Vulder.Timetable.Infrastructure.Redis;

public class RedisModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RedisContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<BranchRepository>()
            .As<IBranchRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TimetableRepository>()
            .As<ITimetableRepository>()
            .InstancePerLifetimeScope();
    }
}