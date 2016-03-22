// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.console.Utilities
{
    using System.Web.Optimization;

    using Autofac;

    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Conventions;

    /// <summary>The bootstrapper.</summary>
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        #region Fields

        /// <summary>The container.</summary>
        private readonly ILifetimeScope container;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Bootstrapper"/> class.</summary>
        /// <param name="container">The container.</param>
        public Bootstrapper(ILifetimeScope container)
        {
            this.container = container;
        }

        #endregion

        #region Properties

        /////// <summary>Gets the internal configuration.</summary>
        ////protected override NancyInternalConfiguration InternalConfiguration
        ////{
        ////    get
        ////    {
        ////        return NancyInternalConfiguration.WithOverrides(x => x.ViewLocationProvider = typeof(ResourceViewLocationProvider));
        ////    }
        ////}

        #endregion

        #region Methods

        /// <summary>The application startup.</summary>
        /// <param name="container">The container.</param>
        /// <param name="pipelines">The pipelines.</param>
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /////// <summary>The configure application container.</summary>
        /////// <param name="existingContainer">The existing container.</param>
        ////protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        ////{
        ////    base.ConfigureApplicationContainer(existingContainer);

        ////    // This should be the assembly your views are embedded in
        ////    var assembly = this.GetType().Assembly;
        ////    ResourceViewLocationProvider.RootNamespaces.Add(assembly, "redis_stat.net.Views");
        ////}

        /// <summary>The configure conventions.</summary>
        /// <param name="nancyConventions">The nancy conventions.</param>
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Content"));
        }

        /// <summary>The get application container.</summary>
        /// <returns>The <see cref="ILifetimeScope"/>.</returns>
        protected override ILifetimeScope GetApplicationContainer()
        {
            return this.container;
        }

        #endregion
    }
}