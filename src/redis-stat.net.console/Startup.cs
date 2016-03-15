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
    using System;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Diagnostics;

    using Owin;

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

            ////// Get your HubConfiguration. In OWIN, you'll create one rather than using GlobalHost.
            var config = new HubConfiguration { Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(this.container) };

            app.UseAutofacMiddleware(this.container);
            ////app.UseAutofacMvc();
            app.MapSignalR("/signalr", config);
            app.UseErrorPage(
                new ErrorPageOptions
                    {
                        ShowCookies = true,
                        ShowEnvironment = true,
                        ShowExceptionDetails = true,
                        ShowHeaders = true,
                        ShowQuery = true,
                        ShowSourceCode = true,
                        SourceCodeLineCount = 5
                    });
            app.UseNancy();
        }

        #endregion
    }
}