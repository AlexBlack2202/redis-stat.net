// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleOutput.cs" company="">
//   
// </copyright>
// <summary>
//   The console output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.console.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using redis_stat.net.common.Models;
    using redis_stat.net.console.Utilities;

    /// <summary>The console output.</summary>
    internal class ConsoleOutput : IOutput
    {
        #region Fields

        /// <summary>The options.</summary>
        private readonly IOptions options;

        /// <summary>The term error reported.</summary>
        private bool termErrorReported;

        /// <summary>The static info reported.</summary>
        private bool staticInfoReported;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ConsoleOutput"/> class.</summary>
        /// <param name="options">The options.</param>
        public ConsoleOutput(IOptions options)
        {
            this.options = options;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The write.</summary>
        /// <param name="stats">The stats.</param>
        public void Write(Stats stats)
        {
            if (!this.staticInfoReported)
            {
                this.OutputStaticInfo(stats);
                this.OutputHeaders();
                this.staticInfoReported = true;
            }

            if (this.OutputTermErrors(stats.Error))
            {
                return;
            }
            
            this.OutputDynamicInfo(stats);
        }

        #endregion

        #region Methods

        /// <summary>The line.</summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string Line(int n)
        {
            return new string('-', n);
        }

        /// <summary>The format exceptions.</summary>
        /// <param name="exceptions">The exceptions.</param>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        private IEnumerable<string> FormatExceptions(IEnumerable<string> exceptions)
        {
            var enumerable = exceptions as string[] ?? exceptions.ToArray();
            if (!enumerable.Any())
            {
                return new string[0];
            }
            else
            {
                var now = DateTime.Now.ToString("YY-MM-DD hh:mm:ss");
                var result = enumerable.Select(exception => string.Format("[{0}@{1}] {2}", now, 0, exception)).ToList();
                return result;
            }
        }

        /// <summary>The move.</summary>
        /// <param name="lines">The lines.</param>
        private void Move(int lines)
        {
            if (lines == 0)
            {
                return;
            }

            Console.Write(@"\e[{0}A\e[0G", lines < 0 ? -lines : lines);
        }

        /// <summary>The output static info.</summary>
        /// <param name="stats">The stats.</param>
        private void OutputDynamicInfo(Stats stats)
        {
            var dynamicProperties = stats.DynamicProperties;
            var measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"];

            var sb = new StringBuilder();
            foreach (var measure in measures)
            {
                sb.AppendFormat(Constants.Format[measure] + " ", dynamicProperties[measure]["sum"][0]);
            }

            Console.WriteLine(sb.ToString());
        }

        /// <summary>The output static info.</summary>
        /// <param name="stats">The stats.</param>
        private void OutputStaticInfo(Stats stats)
        {
            var staticinfo = stats.StaticProperties;
            var list = new List<string[]> { new[] { string.Empty }.Union(this.options.Hosts).ToArray() };
            list.AddRange(staticinfo.Keys.Select(key => new[] { key }.Concat(staticinfo[key]).ToArray()));
            Console.WriteLine(list.GetDataInTableFormat());
        }

        /// <summary>The output headers.</summary>
        private void OutputHeaders()
        {
            var measures = Constants.Measures[this.options.Verbose ? "verbose" : "default"];
            var sb = new StringBuilder();
            var line = Line(90);
            sb.AppendLine(line);
            foreach (var measure in measures)
            {
                sb.AppendFormat(Constants.Format[measure] + " ", Constants.Labels[measure]);
            }

            sb.AppendLine();
            sb.AppendLine(line);

            Console.WriteLine(sb.ToString());
        }

        /// <summary>The output term errors.</summary>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool OutputTermErrors(IEnumerable<object> errorMessages)
        {
            this.termErrorReported = this.termErrorReported || this.termErrorReported == false;
            if (errorMessages == null || !errorMessages.Any())
            {
                this.termErrorReported = false;
            }
            else
            {
                if (this.termErrorReported)
                {
                    Console.Write(string.Empty);
                }

                Console.WriteLine(string.Join("/", errorMessages));
                this.termErrorReported = true;
            }

            return this.termErrorReported;
        }

        #endregion
    }
}