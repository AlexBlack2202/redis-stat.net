// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console
{
    using System;
    using System.Linq.Expressions;

    using Autofac;

    using Microsoft.Owin.Hosting;

    using redis_stat.net.common.Models;

    /// <summary>The program.</summary>
    internal class Program
    {
        #region Methods

        /// <summary>The main.</summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ProgramRegistrationModule>();
            var container = builder.Build();

            var options = container.Resolve<IOptions>();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Server != 0 && options.Daemon)
                {
                    var baseUrl = string.Format("http://+:{0}", options.Server);
                    using (WebApp.Start(baseUrl, app => new Startup(container).Configuration(app)))
                    {
                        Console.WriteLine("Running on {0}", baseUrl);
                        Console.WriteLine("Press enter to exit");
                        Console.ReadLine();
                    }
                }
                else
                {
                    container.Resolve<IRedisStatistics>();
                    Console.ReadLine();
                }
                
            }
        }

        #endregion
    }
}