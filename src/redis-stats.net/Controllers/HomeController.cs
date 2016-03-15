// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
//  <summary>
//   HomeController.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using redis_stat.net.common.Models;
    using redis_stat.net.ViewModels;

    using IndexViewModel = redis_stat.net.ViewModels.IndexViewModel;

    /// <summary>The home controller.</summary>
    public class HomeController : Controller
    {
        /// <summary>The options.</summary>
        private readonly IOptions options;

        private readonly IRedisStatistics redisStatistics;

        #region Public Methods and Operators

        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="redisStatistics">The redis Stats.</param>
        public HomeController(IOptions options, IRedisStatistics redisStatistics)
        {
            this.options = options;
            this.redisStatistics = redisStatistics;
        }

        /// <summary>The index.</summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Index()
        {
            var selected = this.Request.QueryString.Get("host");
            var navigationViewModel = new NavigationViewModel
            {
                Hosts = from host in this.options.Hosts
                        select new Tuple<string, string>(host, "/?host=" + WebUtility.HtmlEncode(host)),
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

            return this.View(viewModel);
        }

        #endregion
    }
}