// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentOptions.cs" company="">
//   
// </copyright>
// <summary>
//   The argument options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console.Models
{
    using CommandLine;
    using CommandLine.Text;

    using redis_stat.net.common.Models;

    /// <summary>The argument options.</summary>
    internal class ArgumentOptions : IOptions
    {
        #region Public Properties

        /// <summary>Gets or sets the CSV.</summary>
        [Option("csv", DefaultValue = "", HelpText = "Save the result in CSV format")]
        public string Csv { get; set; }

        /// <summary>Gets or sets the count.</summary>
        [Option("count", DefaultValue = 0, HelpText = "Number of times ")]
        public int Count { get; set; }

        /// <summary>Gets or sets a value indicating whether daemon.</summary>
        [Option("daemon", DefaultValue = false, HelpText = "Daemonize redis-stat.net. Must be used with --server option.")]
        public bool Daemon { get; set; }

        /// <summary>Gets or sets the hosts.</summary>
        [OptionArray("hosts", DefaultValue = new[] { "localhost:6397" }, HelpText = "Hosts")]
        public string[] Hosts { get; set; }

        /// <summary>Gets or sets the interval.</summary>
        [Option("interval", DefaultValue = 2, HelpText = "The interval")]
        public int Interval { get; set; }

        /// <summary>Gets or sets the password.</summary>
        [Option('a', "auth", DefaultValue = "", HelpText = "Password")]
        public string Password { get; set; }

        /// <summary>Gets or sets the server.</summary>
        [Option("server", DefaultValue = 63790, HelpText = "Launch redis-stat.net web server (default port: 63790)")]
        public int Server { get; set; }

        /// <summary>Gets or sets a value indicating whether verbose.</summary>
        [Option('v', "verbose", DefaultValue = false, HelpText = "Show more info")]
        public bool Verbose { get; set; }

        /// <summary>Gets or sets the style.</summary>
        [Option("style", DefaultValue = "unicode", HelpText = "Output style: unicode|ascii")]
        public string Style { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The get usage.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        [HelpOption(HelpText = "Show this message")]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        #endregion
    }
}