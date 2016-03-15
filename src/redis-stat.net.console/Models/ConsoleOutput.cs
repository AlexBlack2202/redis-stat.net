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
    using System.IO;
    using System.Linq;
    using System.Text;

    using redis_stat.net.console.Utilities;

    using redis_stats.net.common.Models;

    using WebGrease.Css.Extensions;

    /// <summary>The console output.</summary>
    internal class ConsoleOutput : IOutput
    {
        private readonly IOptions options;

        public ConsoleOutput(IOptions options)
        {
            this.options = options;
        }

        /// <summary>The term error reported.</summary>
        private bool termErrorReported;

        #region Public Methods and Operators

        /// <summary>The write.</summary>
        /// <param name="stats">The stats.</param>
        public void Write(Stats stats)
        {
            this.OutputDynamicInfo(stats);
            ////this.OutputStaticInfo(stats);
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

        private void OutputFile(Stats stats, StreamWriter file)
        {

        }

        private bool OutputTermErrors(IEnumerable<string> errorMessages)
        {
            this.termErrorReported = this.termErrorReported || this.termErrorReported == false;
            if (!errorMessages.Any())
            {
                this.termErrorReported = false;
            }
            else
            {
                if (this.termErrorReported)
                {
                    Console.Write(string.Empty);
                }

                Console.Write(string.Join("/", errorMessages));
                this.termErrorReported = true;
            }

            return this.termErrorReported;
        }

        private void OutputTerm(Stats stat, IEnumerable<string> errorMessages)
        {
            if(this.OutputTermErrors(errorMessages))
            {
                return;
            }
        }

        /// <summary>The output static info.</summary>
        /// <param name="stats">The stats.</param>
        private void OutputStaticInfo(Stats stats)
        {
            var staticinfo = stats.StaticProperties;
            var list = new List<string[]> { new[] { string.Empty }.Union(this.options.Hosts).ToArray() };
            list.AddRange(staticinfo.Keys.Select(key => new[] { key }.Union(staticinfo[key]).ToArray()));
            Console.WriteLine(list.GetDataInTableFormat());
        }

        private static string Line(int n)
        {
            return new string('-', n);
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



        #endregion
    }
}