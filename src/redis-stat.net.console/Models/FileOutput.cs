// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileOutput.cs" company="">
//   
// </copyright>
// <summary>
//   The file output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console.Models
{
    using System.Linq;

    using redis_stats.net.common.Models;

    /// <summary>The file output.</summary>
    internal class FileOutput : IOutput
    {
        /// <summary>The options.</summary>
        private readonly IOptions options;

        /// <summary>Initializes a new instance of the <see cref="FileOutput"/> class.</summary>
        /// <param name="options">The options.</param>
        public FileOutput(IOptions options)
        {
            this.options = options;
        }

        #region Public Methods and Operators

        /// <summary>The write.</summary>
        /// <param name="stats">The stats.</param>
        public void Write(Stats stats)
        {
            var fileManager = new FileManager();
            var dynamicProperties = stats.DynamicProperties;
            var measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"];
            var list = measures.Select(measure => dynamicProperties[measure]["sum"][0].ToString()).ToList();
            fileManager.WriteFile(this.options.Csv, string.Join(",", list));
        }

        #endregion
    }
}