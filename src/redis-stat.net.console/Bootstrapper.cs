// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console
{
    using Autofac;

    using Nancy.Bootstrappers.Autofac;

    /// <summary>The bootstrapper.</summary>
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        #region Methods

        /// <summary>Initializes a new instance of the <see cref="Bootstrapper"/> class.</summary>
        /// <param name="container">The container.</param>
        public Bootstrapper(ILifetimeScope container)
        {
        }

        /// <summary>The configure application container.</summary>
        /// <param name="container">The container.</param>
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => builder.RegisterType<SmtpService>().As<ISmtpService>().SingleInstance());
        }

        #endregion
    }
}