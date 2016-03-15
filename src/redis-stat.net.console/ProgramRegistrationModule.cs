// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgramRegistrationModule.cs" company="">
//   
// </copyright>
// <summary>
//   The program registration module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console
{
    using System.Reflection;

    using Autofac;
    using Autofac.Extras.AttributeMetadata;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.SignalR;

    using redis_stat.net.common.Hubs;
    using redis_stat.net.common.Models;
    using redis_stat.net.console.Models;
    using redis_stat.net.Models;

    using Module = Autofac.Module;

    /// <summary>The program registration module.</summary>
    public class ProgramRegistrationModule : Module
    {
        #region Methods

        /// <summary>The load.</summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly(); 
            
            // Register your MVC controllers.
            builder.RegisterControllers(assembly);

            // OPTIONAL: Register model binders that require DI.
            ////builder.RegisterModelBinders(assembly);
            ////builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            ////builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            ////builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            ////builder.RegisterFilterProvider();

            builder.RegisterHubs(assembly);
            builder.RegisterType<ArgumentOptions>().As<IOptions>().SingleInstance();

            builder.RegisterType<FileOutput>().As<IOutput>().Keyed<IOutput>("csv").SingleInstance();
            builder.RegisterType<SignalROutput>().As<IOutput>().Keyed<IOutput>("daemon").SingleInstance();
            builder.RegisterType<ConsoleOutput>().As<IOutput>().Keyed<IOutput>("console").SingleInstance();

            builder.RegisterType<RedisClient>().As<IRedisClient>().SingleInstance();
            builder.RegisterType<RedisStatistics>().As<IRedisStatistics>().WithAttributeFilter().SingleInstance();
            builder.RegisterType<RedisStatsHub>().ExternallyOwned();

            base.Load(builder);
        }

        #endregion
    }
}