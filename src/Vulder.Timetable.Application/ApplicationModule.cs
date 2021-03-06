using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Vulder.Timetable.Application.Branch;
using Vulder.Timetable.Application.Classname;
using Vulder.Timetable.Application.Timetable;
using Module = Autofac.Module;

namespace Vulder.Timetable.Application;

public class ApplicationModule : Module
{
    private readonly List<Assembly?> _assemblies = new();

    public ApplicationModule()
    {
        _assemblies.Add(Assembly.GetAssembly(typeof(BranchesRequestHandler)));
        _assemblies.Add(Assembly.GetAssembly(typeof(TimetableRequestHandler)));
        _assemblies.Add(Assembly.GetAssembly(typeof(ResolveClassRequestHandler)));
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder.Register<ServiceFactory>(context =>
        {
            var c = context.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });

        var mediatorOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionHandler<,>),
            typeof(INotificationHandler<>)
        };

        foreach (var mediatorOpenType in mediatorOpenTypes)
            builder.RegisterAssemblyTypes(_assemblies.ToArray()!)
                .AsClosedTypesOf(mediatorOpenType)
                .AsImplementedInterfaces();
    }
}