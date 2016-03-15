// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationViewModel.cs" company="">
//   
// </copyright>
//  <summary>
//   NavigationViewModel.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>The navigation view model.</summary>
    public class NavigationViewModel
    {
        /// <summary>Initializes a new instance of the <see cref="NavigationViewModel"/> class.</summary>
        public NavigationViewModel()
        {
            this.Hosts = new List<Tuple<string, string>>();
        }

        #region Public Properties

        /// <summary>Gets or sets the hosts.</summary>
        public IEnumerable<Tuple<string, string>> Hosts { get; set; }

        /// <summary>Gets or sets the selected.</summary>
        public string Selected { get; set; }

        #endregion
    }
}