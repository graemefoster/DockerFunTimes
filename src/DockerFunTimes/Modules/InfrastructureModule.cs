using System;
using Autofac;
using DockerFunTimes.Infrastructure;

namespace DockerFunTimes.Modules
{
    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configuration = new ConfigurationSettings();
            builder.RegisterInstance(configuration);

            builder.RegisterType<Storage>()
                .InstancePerLifetimeScope().AsImplementedInterfaces();

            if (configuration.Configuration.UseRabbit)
            {
                Console.WriteLine("Registered the rabbit");
                builder.RegisterType<Queue>().InstancePerLifetimeScope().AsImplementedInterfaces();
            }
            else
            {
                Console.WriteLine("Registered the fake");
                builder.RegisterType<FakeQueue>().InstancePerLifetimeScope().AsImplementedInterfaces();
            }
        }
    }
}