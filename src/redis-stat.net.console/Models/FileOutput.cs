// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileOutput.cs" company="">
//   
// </copyright>
// <summary>
//   The file output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.common.IO
{
    using System.Linq;

    using redis_stat.net.common.Models;

    /// <summary>The file output.</summary>
    internal class FileOutput : IOutput
    {
        #region Fields

        /// <summary>The file manager.</summary>
        private readonly IFileManager fileManager;

        /// <summary>The options.</summary>
        private readonly IOptions options;

        /// <summary>The static info reported.</summary>
        private bool staticInfoReported;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="FileOutput"/> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="fileManager">The file Manager.</param>
        public FileOutput(IOptions options, IFileManager fileManager)
        {
            this.options = options;
            this.fileManager = fileManager;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The write.</summary>
        /// <param name="stats">The stats.</param>
        public void Write(Stats stats)
        {
            if (!this.staticInfoReported)
            {
                this.OutputHeaders();
                this.staticInfoReported = true;
            }

            this.OutputDynamicInfo(stats);
        }

        #endregion

        #region Methods

        /// <summary>The output dynamic info.</summary>
        /// <param name="stats">The stats.</param>
        private void OutputDynamicInfo(Stats stats)
        {
            var dynamicProperties = stats.DynamicProperties;
            var measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"];
            var list = measures.Select(measure => dynamicProperties[measure]["sum"][0].ToString()).ToList();
            this.fileManager.WriteFile(this.options.Csv, string.Join(",", list));
        }

        /// <summary>The output headers.</summary>
        private void OutputHeaders()
        {
            var measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"];
            var list = measures.Select(measure => Constants.Labels[measure]);
            this.fileManager.WriteFile(this.options.Csv, string.Join(",", list));
        }

        #endregion
    }
}