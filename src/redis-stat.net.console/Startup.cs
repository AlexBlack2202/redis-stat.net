// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="">
//   
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Owin;

[assembly: OwinStartup(typeof(redis_stat.net.console.Startup))]

namespace redis_stat.net.console
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Microsoft.Owin.Cors;

    using Nancy.Owin;

    using Owin;

    using redis_stat.net.console.Utilities;

    /// <summary>The startup.</summary>
    public class Startup
    {
        #region Fields

        /// <summary>The container.</summary>
        private readonly ILifetimeScope container;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="container">The container.</param>
        public Startup(ILifetimeScope container)
        {
            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The configuration.</summary>
        /// <param name="app">The app.</param>
        public void Configuration(IAppBuilder app)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(this.container));

            app.UseAutofacMiddleware(this.container);
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

            var options = new NancyOptions { Bootstrapper = new Bootstrapper(this.container) };
            app.UseNancy(options);
        }

        #endregion
    }
}