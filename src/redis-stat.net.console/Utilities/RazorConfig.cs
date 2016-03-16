// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RazorConfig.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RazorConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console.Utilities
{
    using System.Collections.Generic;

    using Nancy.ViewEngines.Razor;

    /// <summary>The razor config.</summary>
    public class RazorConfig : IRazorConfiguration
    {
        /// <summary>The get assembly names.</summary>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "Newtonsoft.Json";
            yield return "redis-stat.net.common";
            yield return "System.Web.Optimization";
            yield return "System.Web";
            yield return "System.Web.Mvc";
            yield return "Nancy.ViewEngines.Razor";
        }

        /// <summary>The get default namespaces.</summary>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "redis_stat.net.ViewModels";
            yield return "redis_stat.net.common";
            yield return "redis_stat.net.common.Models";
            yield return "System.Web";
            yield return "System.Web.Mvc";
            yield return "Nancy.ViewEngines.Razor";
        }

        /// <summary>Gets a value indicating whether auto include model namespace.</summary>
        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
