using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DDDPerth.Modules;

namespace DDDPerth
{
    public class Example4
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();
 
            host.Run();
        }
    }

    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
 
        public IServiceProvider ConfigureServices(IServiceCollection services) {

            services.AddMvc()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(options => {
                    options.Filters.Add(typeof(ReturnValidationErrorsFilter));
                });

            services.AddSwaggerGen();

            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ApplicationModule>();

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole();

            app.UseMvc();
            app.UseSwaggerUi();
            app.UseSwagger();

            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
   }}
