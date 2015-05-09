// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexViewModel.cs" company="">
//   
// </copyright>
//  <summary>
//   IndexViewModel.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using redis_stat.net.Models;

    /// <summary>The index view model.</summary>
    public class IndexViewModel
    {
        #region Fields

        /// <summary>The verbose.</summary>
        private readonly bool verbose;

        private readonly int historyLength;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="IndexViewModel"/> class.</summary>
        /// <param name="verbose">The verbose.</param>
        /// <param name="historyLength">The history Length.</param>
        public IndexViewModel(bool verbose, int historyLength)
        {
            this.verbose = verbose;
            this.historyLength = historyLength;
            this.Measures = new List<string>();
            this.TabMeasures = new List<string>();
            this.Navigation = new NavigationViewModel();
            this.History = new List<Stats>();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the additional classes.</summary>
        public string AdditionalClasses
        {
            get
            {
                var classes = new StringBuilder();
                return classes.ToString();
            }
        }

        /// <summary>Gets the colors.</summary>
        public Dictionary<string, string> Colors
        {
            get
            {
                return
                    this.Measures.Select(
                        m => new KeyValuePair<string, string>(m, string.Join(" ", Constants.Colors[m])))
                        .ToDictionary(a => a.Key, b => b.Value);
            }
        }

        /// <summary>Gets or sets the history.</summary>
        public IEnumerable<Stats> History { get; set; }

        /// <summary>Gets the stat table rows.</summary>
        public int HistoryLength
        {
            get
            {
                return this.historyLength;
            }
        }

        /// <summary>Gets the info.</summary>
        public Dictionary<string, Dictionary<string, string[]>> Info
        {
            get
            {
                var history = this.History.ToList();
                return this.Navigation.Hosts.ToDictionary(
                    host => host.Text,
                    host =>
                        {
                            var firstOrDefault = history.FirstOrDefault();
                            return firstOrDefault != null ? firstOrDefault.StaticProperties.ToDictionary(a => a.Key, b => b.Value) : new Dictionary<string, string[]>();
                        });
            }
        }

        /// <summary>Gets or sets the interval.</summary>
        public int Interval { get; set; }

        /// <summary>Gets or sets the measures.</summary>
        public IEnumerable<string> Measures { get; set; }

        /// <summary>Gets or sets the navigation.</summary>
        public NavigationViewModel Navigation { get; set; }

        /// <summary>Gets the stat table rows.</summary>
        public int StatTableRows
        {
            get
            {
                return 10;
            }
        }

        /// <summary>Gets or sets the tab measures.</summary>
        public IEnumerable<string> TabMeasures { get; set; }

        /// <summary>Gets the additional classes.</summary>
        public string VerboseClass
        {
            get
            {
                return this.verbose ? " verbose" : string.Empty;
            }
        }

        #endregion
    }
}