using System.Collections.Generic;
using Autofac;
using MediatR;

namespace DDDPerth.Modules
{
    public class InfrastructureModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register<SingleInstanceFactory>(ctx =>
                    {
                        var c = ctx.Resolve<IComponentContext>();
                        return t => c.Resolve(t);
                    });

            builder.Register<MultiInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                });

            builder.RegisterType<MultiInstanceFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}