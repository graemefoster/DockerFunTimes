using System;
using System.IO;
using System.Threading;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DockerFunTimes.Features.Fun;
using DockerFunTimes.Infrastructure;
using DockerFunTimes.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace DockerFunTimes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Yukky -
            UpdateDatabase();

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static void UpdateDatabase()
        {
            bool keepTrying = true;
            while (keepTrying)
            {
                try
                {
                    using (
                        var connection =
                            new MySql.Data.MySqlClient.MySqlConnection(
                                new ConfigurationSettings().Configuration.DatabaseConnection))
                    {
                        connection.Open();
                        ExecCommand(connection, "create database if not exists FunTimes;");
                        ExecCommand(connection, "use FunTimes;");
                        ExecCommand(connection, @"create table if not exists Foo (
    Id integer auto_increment primary key,
    Name varchar(100),
    DateOfBirth date
                                            );");
                    }
                    keepTrying = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(5000);
                }
            }
        }

        private static void ExecCommand(MySqlConnection connection, string sql)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(options => {
                    options.Filters.Add(typeof(ReturnValidationErrorsFilter));
                });

            services.AddSwaggerGen();

            var sqlconbuilder =
                new MySqlConnectionStringBuilder(new ConfigurationSettings().Configuration.DatabaseConnection)
                {
                    Database = "FunTimes"
                };

            services.AddDbContext<TerribleEntityContext>(
                options => options.UseMySQL(sqlconbuilder.ConnectionString));

            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<MediatorModule>();
            builder.RegisterModule<ApplicationModule>();

            builder.Populate(services);

            ApplicationContainer = builder.Build();

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
    }
}
