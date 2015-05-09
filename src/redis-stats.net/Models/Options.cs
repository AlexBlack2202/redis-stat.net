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

    /// <summary>The options.</summary>
    public class Options : IOptions
    {
        /// <summary>Gets the interval.</summary>
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
        }

        /// <summary>Gets a value indicating whether verbose.</summary>
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
        }

        /// <summary>Gets the password.</summary>
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
        }

        /// <summary>Gets the password.</summary>
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
        }


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
    }
}