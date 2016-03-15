// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="">
//   
// </copyright>
//  <summary>
//   Options.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.Models
{
    using System;
    using System.Configuration;

    using redis_stat.net.common.Models;

    /// <summary>The options.</summary>
    public class Options : IOptions
    {
        #region Public Properties

        /// <summary>Gets or sets the count.</summary>
        public int Count { get; set; }

        /// <summary>Gets or sets the CSV.</summary>
        public string Csv { get; set; }

        /// <summary>Gets or sets a value indicating whether daemon.</summary>
        public bool Daemon { get; set; }

        /// <summary>Gets or sets the hosts.</summary>
        public string[] Hosts
        {
            get
            {
                string[] result = { "127.0.0.1:6379" };
                string value;
                if (this.GetValue("Hosts", out value))
                {
                    result = value.Split(',');
                }

                return result;
            }

            set
            {
            }
        }

        /// <summary>Gets or sets the interval.</summary>
        public int Interval
        {
            get
            {
                int result = 2; // Default Interval
                string value;
                if (this.GetValue("Interval", out value))
                {
                    int.TryParse(value, out result);
                }

                return result;
            }

            set
            {
            }
        }

        /// <summary>Gets or sets the password.</summary>
        public string Password
        {
            get
            {
                string result = null;
                string value;
                if (this.GetValue("RedisPassword", out value))
                {
                    result = value;
                }

                return result;
            }
            set
            {
            }
        }

        /// <summary>Gets or sets the server.</summary>
        public int Server { get; set; }

        /// <summary>Gets or sets the style.</summary>
        public string Style { get; set; }

        /// <summary>Gets or sets a value indicating whether verbose.</summary>
        public bool Verbose
        {
            get
            {
                bool result = false;
                string value;
                if (this.GetValue("Verbose", out value))
                {
                    bool.TryParse(value, out result);
                }

                return result;
            }
            set
            {
            }
        }

        #endregion

        #region Methods

        /// <summary>The get value.</summary>
        /// <param name="key">The key.</param>
        /// <param name="result">The result.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool GetValue(string key, out string result)
        {
            try
            {
                result = ConfigurationManager.AppSettings[key];
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        #endregion
    }
}