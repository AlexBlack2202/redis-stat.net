// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="">
//   
// </copyright>
//  <summary>
//   Startup.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Owin;

using redis_stat.net;

[assembly: OwinStartup(typeof(Startup))]

namespace redis_stat.net
{
    using System.Web.Mvc;

    using Autofac.Integration.Mvc;

    using Owin;

    /// <summary>The startup.</summary>
    public class Startup
    {
        #region Public Methods and Operators

        /// <summary>The configuration.</summary>
        /// <param name="app">The app.</param>
        public void Configuration(IAppBuilder app)
        {
            var resolver = DependencyResolver.Current as AutofacDependencyResolver;
            if (resolver != null)
            {
                app.UseAutofacMiddleware(resolver.ApplicationContainer);
            }

            app.MapSignalR();
        }

        #endregion
    }
}