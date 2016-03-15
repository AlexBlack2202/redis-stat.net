namespace redis_stat.net
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Microsoft.AspNet.SignalR;

    using redis_stat.net.common.Hubs;
    using redis_stat.net.common.Models;
    using redis_stat.net.Models;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();

            builder.RegisterType<Options>().As<IOptions>();
            builder.RegisterType<SignalROutput>().As<IOutput>();
            builder.RegisterType<RedisClient>().As<IRedisClient>().SingleInstance();
            builder.RegisterType<RedisStatistics>().As<IRedisStatistics>().As<IStartable>().SingleInstance();
            builder.RegisterType<RedisStatsHub>().ExternallyOwned();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            // Configure MVC with the dependency resolver.
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(resolver.RequestLifetimeScope);
        }
    }
}
