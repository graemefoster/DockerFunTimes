using System.Linq;
using System.Reflection;
using Autofac;
using DDDPerth.Features.Assessment;
using DockerFunTimes.Features.Fun;
using MediatR;

namespace DockerFunTimes.Modules
{
    public class ApplicationModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .As(t => t.GetInterfaces()
                    .Where(a => a.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                    .Select(a => new Autofac.Core.KeyedService("commandHandler", a)));

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .As(t => t.GetInterfaces()
                    .Where(a => a.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>))));

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsClosedTypesOf(typeof(IValidate<>));

            builder.RegisterGenericDecorator(
                    typeof(ValidationDecorator<,>),
                    typeof(IRequestHandler<,>),
                    fromKey: "commandHandler");
        }
    }
}