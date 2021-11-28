using Autofac;
using Vulder.Timetable.Infrastructure.Redis;

namespace Vulder.Timetable.Infrastructure;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule(new RedisModule());
    }
}