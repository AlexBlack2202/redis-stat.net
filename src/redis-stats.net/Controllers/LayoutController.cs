// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayoutController.cs" company="">
//   
// </copyright>
//  <summary>
//   LayoutController.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using redis_stat.net.ViewModels;

    using redis_stats.net.common.Models;

    /// <summary>The layout controller.</summary>
    public class LayoutController : Controller
    {
        /// <summary>The options.</summary>
        private readonly IOptions options;

        /// <summary>Initializes a new instance of the <see cref="LayoutController"/> class. </summary>
        /// <param name="options">The options.</param>
        public LayoutController(IOptions options)
        {
            this.options = options;
        }

        #region Public Methods and Operators

        /// <summary>The navigation.</summary>
        /// <returns>The <see cref="PartialViewResult" />.</returns>
        public PartialViewResult Navigation()
        {
            var selected = this.Request.QueryString.Get("host");
            var navigationViewModel = new NavigationViewModel
                                          {
                                              Hosts = from host in this.options.Hosts
                                                      select new SelectListItem { Text = host, Value = "/?host=" + WebUtility.HtmlEncode(host) },
                                              Selected = selected
                                          };
            return this.PartialView("_Navigation", navigationViewModel);
        }

        #endregion
    }
}