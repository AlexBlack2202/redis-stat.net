// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOptions.cs" company="">
//   
// </copyright>
// <summary>
//   The Options interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.common.Models
{
    /// <summary>The Options interface.</summary>
    public interface IOptions
    {
        #region Public Properties

        /// <summary>Gets or sets the count.</summary>
        int Count { get; set; }

        /// <summary>Gets or sets the CSV.</summary>
        string Csv { get; set; }

        /// <summary>Gets or sets a value indicating whether daemon.</summary>
        bool Daemon { get; set; }

        /// <summary>Gets or sets the hosts.</summary>
        string[] Hosts { get; set; }

        /// <summary>Gets or sets the interval.</summary>
        int Interval { get; set; }

        /// <summary>Gets or sets the password.</summary>
        string Password { get; set; }

        /// <summary>Gets or sets the server.</summary>
        int Server { get; set; }

        /// <summary>Gets or sets the style.</summary>
        string Style { get; set; }

        /// <summary>Gets or sets a value indicating whether verbose.</summary>
        bool Verbose { get; set; }

        #endregion
    }
}