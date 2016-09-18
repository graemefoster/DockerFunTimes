using System.Linq;
using System.Reflection;
using Autofac;
using DDDPerth.Features.Assessment;
using MediatR;

namespace DDDPerth.Modules
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
                    .AsClosedTypesOf(typeof(IValidate<>));

            builder.RegisterGenericDecorator(
                    typeof(ValidationDecorator<,>),
                    typeof(IRequestHandler<,>),
                    fromKey: "commandHandler");

        }
    }
}