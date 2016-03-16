// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeModule.cs" company="">
//   
// </copyright>
// <summary>
//   The home module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.console.Modules
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Nancy;

    using redis_stat.net.common.Models;
    using redis_stat.net.ViewModels;

    /// <summary>The home module.</summary>
    public class HomeModule : NancyModule
    {
        #region Fields

        /// <summary>The options.</summary>
        private readonly IOptions options;

        /// <summary>The redis statistics.</summary>
        private readonly IRedisStatistics redisStatistics;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="HomeModule"/> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="redisStatistics">The redis statistics.</param>
        public HomeModule(IOptions options, IRedisStatistics redisStatistics)
        {
            this.options = options;
            this.redisStatistics = redisStatistics;

            this.Get["/"] = parameters =>
                {

                    var selected = this.Request.Form["host"];
                    var navigationViewModel = new NavigationViewModel
                                                  {
                                                      Hosts = from host in this.options.Hosts select new Tuple<string, string>(host, "/?host=" + WebUtility.HtmlEncode(host)),
                                                      Selected = selected
                                                  };

                    var viewModel = new IndexViewModel(this.options.Verbose, Constants.HistoryLength)
                                        {
                                            Measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"],
                                            TabMeasures = Constants.Measures["static"],
                                            Interval = this.options.Interval,
                                            History = this.redisStatistics.GetAllStats().Take(Constants.HistoryLength),
                                            Navigation = navigationViewModel
                                        };

                    return this.View["Index.cshtml", viewModel];
                };
        }

        #endregion
    }
}