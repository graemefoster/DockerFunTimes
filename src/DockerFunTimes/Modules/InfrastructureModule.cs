using Autofac;
using DockerFunTimes.Infrastructure;

namespace DockerFunTimes.Modules
{
    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationSettings>()
                .SingleInstance().AsSelf();

            builder.RegisterType<Storage>()
                .InstancePerLifetimeScope().AsImplementedInterfaces();
        }
    }
}